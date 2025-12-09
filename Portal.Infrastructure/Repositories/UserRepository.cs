using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Portal.Infrastructure.Repositories
{
    public class UserRepository : IUserDomain
    {
        private readonly PortalDbContext context;

        public UserRepository(PortalDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="request">Объект класса LoginDTO</param>
        /// <returns></returns>
        public async Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            if (request is null)
                return new CustomAuthResponses(false, $"Пользователь равен null.");

            var userDB = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (userDB is null || userDB.IsActive == false)
                return new CustomAuthResponses(false, $"Проверьте введённые данные.");

            if (new PasswordHasher<User>().VerifyHashedPassword(userDB, userDB.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return new CustomAuthResponses(false, "Проверьте введённые данные.");

            var userRole = await context.UserRoles.FirstOrDefaultAsync(r => r.Id == userDB.UserRoleId);

            userDB.UserRole = userRole;

            var jwtToken = CreateToken(userDB);

            await GenerateAndSaveTokensAsync(userDB, jwtToken);

            return new CustomAuthResponses(true, $"Пользователь {userDB.Username} успешно авторизован.", jwtToken);
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="request">Объект класса RegisterDTO</param>
        /// <returns></returns>
        public async Task<CustomGeneralResponses> RegisterAsync(RegisterDTO request)
        {
            if (request is null)
                return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var userDB = await context.Users.AnyAsync(u => u.Username == request.Username);
            if (userDB)
                return new CustomGeneralResponses(false, "Такой пользователь уже зарегистрирован.");

            var newUser = new User()
            {
                UserDepartmentId = request.UserDepartmentId,
                UserRoleId = request.UserRoleId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Patronymic = request.Patronymic,
                Username = request.Username,
                Email = request.Email,
            };
            newUser.PasswordHash = new PasswordHasher<User>().HashPassword(newUser, request.Password);

            context.Users.Add(newUser);

            var department = await context.UserDepartments.FirstOrDefaultAsync(d => d.Id == request.UserDepartmentId);

            //Создаём автоматически склад пользователя
            var userWarehouse = new UserWarehouse()
            {
                UserId = newUser.Id,
                Title = department!.Title
            };

            context.UserWarehouses.Add(userWarehouse);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Пользователь успешно добавлен.", newUser);
        }

        /// <summary>
        /// Генерируем и сохраняем токен доступа
        /// </summary>
        /// <param name="user">Объект класса User</param>
        /// <param name="jwtToken">JWT токен доступа</param>
        /// <returns></returns>
        private async Task GenerateAndSaveTokensAsync(User user, string jwtToken)
        {
            var userTokens = await context.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id);

            if (userTokens != null)
            {
                userTokens.RefreshToken = GenerateRefreshToken();
                userTokens.DateTimeExpiredToken = DateTime.Now.AddHours(8);
            }
            else
            {
                var refreshToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = GenerateRefreshToken(),
                    DateTimeExpiredToken = DateTime.Now.AddHours(8)
                };
                context.UserTokens.Add(refreshToken);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Создаём токен доступа для пользователя
        /// </summary>
        /// <param name="user">Объект класса User</param>
        /// <returns>Возвращаем токен доступа</returns>
        private string CreateToken(User user)
        {
            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole.Title)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("O]N3SEuo{WnvC~<rcw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDSEuo{WnvC~<rcw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@oh–zw^baX}cw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@f0zdl@oh–zw^baX}cw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@oh–zw^baX}"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: "Turov Dairy Industrial Complex",
                audience: "Employees Turov Dairy Industrial Complex",
                claims: claimsList,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        /// <summary>
        /// Генерируем рефреш токен
        /// </summary>
        /// <returns>Рефреш токен</returns>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Добавляем роль для пользователей
        /// </summary>
        /// <param name="request">Объект класса UserRoleDTO</param>
        /// <returns></returns>
        public async Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request)
        {
            if (request is null)
                return new CustomGeneralResponses(false, "Проверьте правильность введённых данных.");

            var roleTitle = request.Title.ToLower();
            var result = await context.UserRoles.AnyAsync(r => r.Title == roleTitle);

            if (result)
                return new CustomGeneralResponses(false, "Такая роль уже существует.");

            var userRole = new UserRole()
            {
                Title = roleTitle,
                PublicTitle = request.PublicTitle,
            };

            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Роль {roleTitle} успешно создана.");
        }

        /// <summary>
        /// Получаем всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserView>> GetAllAsync()
        {
            var users = await context.Users.Where(u => u.IsActive == true).OrderBy(u => u.LastName).ToListAsync();
            var userslist = new List<UserView>();
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    var department = await context.UserDepartments.FindAsync(user.UserDepartmentId);
                    if (department != null)
                    {
                        department.Users = null!;
                    }
                    UserView userView = new(user.Id, user.UserRoleId, user.UserDepartmentId, user.FirstName, user.LastName,
                                            user.Patronymic, user.Specialization, user.Email, user.IsActive);
                    userslist.Add(userView);
                }
            }
            return userslist;
        }

        /// <summary>
        /// Получаем пользователя по id
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Объект класса User</returns>
        public async Task<UserView> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return null!;

            var user = await context.Users.FindAsync(id);
            if (user is null) return null!;

            var userRole = await context.UserRoles.FindAsync(user.UserRoleId);
            if (userRole != null)
                user.UserRole = userRole;

            UserView userView = new(user.Id, user.UserRoleId, user.UserDepartmentId, user.FirstName, user.LastName,
                                    user.Patronymic, user.Specialization, user.Email, user.IsActive);

            return userView;
        }

        /// <summary>
        /// Получаем все роли пользователей
        /// </summary>
        /// <returns>Список ролей пользователей</returns>
        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            return await context.UserRoles.ToListAsync();
        }

        /// <summary>
        /// Синхронизируем пользователей из Active Directory
        /// </summary>
        /// <returns></returns>
        public async Task<CustomGeneralResponses> SyncUsersAsync()
        {
            try
            {
                List<User> users = new();
                string activeDirectory = "turovmilk.by";
                PrincipalContext principalContext = new(ContextType.Domain, activeDirectory);
                UserPrincipal userPrincipal = new(principalContext);
                PrincipalSearcher search = new(userPrincipal);
                foreach (UserPrincipal result in search.FindAll().Cast<UserPrincipal>())
                {

                    string surname = Convert.ToString(result.Surname);
                    string name = Convert.ToString(result.GivenName);
                    string patronymic = Convert.ToString(result.MiddleName);
                    string username = Convert.ToString(result.SamAccountName);
                    string email = Convert.ToString(result.EmailAddress);

                    if (surname is null)
                    {
                        surname = "";
                    }
                    if (patronymic is null)
                    {
                        patronymic = "";
                    }

                    if (result.Enabled == true & result.EmailAddress != null)
                    {
                        var user = await context.Users.FirstOrDefaultAsync(e => e.Username == username);
                        if (user is null)
                        {
                            var newUser = new User()
                            {
                                UserDepartmentId = Guid.Parse("D7FA6B79-CF7B-442E-32E6-08DDD5A32CAC"),
                                UserRoleId = Guid.Parse("D7FA6B79-CF7B-442E-37E6-08DDD5A32CAC"),
                                FirstName = name,
                                LastName = surname,
                                Patronymic = patronymic,
                                Username = username,
                                PasswordHash = "AQAAAAIAAYagAAAAEOVSg/5PKFU0eFXRm9R6j5GvdEhsxlIymU+I51+5Y/+gQX+c7AHCeu/ZT5ByOLFk7w==",
                                Email = email,
                                IsActive = true
                            };

                            users.Add(newUser);
                        }
                        else
                        {
                            user.FirstName = name;
                            user.LastName = surname;
                            user.Username = username;
                            user.Email = email;
                            user.IsActive = true;
                        }
                    }
                    else if (result.Enabled == false)
                    {
                        var user = context.Users.FirstOrDefault(e => e.Username == username & e.IsActive == true);
                        if (user != null)
                        {
                            user.IsActive = false;
                            await context.SaveChangesAsync();
                            return new CustomGeneralResponses(true, "Пользователь успешно отключён.");
                        }
                    }
                }
                search.Dispose();
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
                return new CustomGeneralResponses(true, "Пользователи успешно синхронизированы.");
            }
            catch (Exception ex) { return new CustomGeneralResponses(false, ex.Message); }
        }

        /// <summary>
        /// Получаем пользователя по username
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Объект класса UserView</returns>
        public async Task<UserView> GetByUsernameAsync(string username)
        {
            if (username == string.Empty) return null!;
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return null!;
            var department = await context.UserDepartments.FindAsync(user.UserDepartmentId);
            if (department != null)
            {
                department.Users = null!;
            }
            UserView userView = new(user.Id, user.UserRoleId, user.UserDepartmentId, user.FirstName, user.LastName,
                                    user.Patronymic, user.Specialization, user.Email, user.IsActive);
            return userView;
        }

        /// <summary>
        /// Редактирование данных пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<CustomGeneralResponses> EditUserAsync(UserView request)
        {
            try
            {
                if (request is null) return new CustomGeneralResponses(false, "Объект равен null.");
                var user = await context.Users.FindAsync(request.Id);
                if (user is null) return new CustomGeneralResponses(false, "Пользователь с переданным ID не найден");

                user.UserRoleId = request.UserRoleId;
                user.UserDepartmentId = request.UserDepartmentId;
                user.FirstName = request.FirstName!;
                user.LastName = request.LastName!;
                user.Patronymic = request.Patronymic!;
                user.Specialization = request.Specialization!;
                user.Email = request.Email!;
                user.IsActive = request.IsActive;


                await context.SaveChangesAsync();
                return new CustomGeneralResponses(true, "Данные о пользователе успешно обновлены!");
            }
            catch (Exception ex)
            {
                return new CustomGeneralResponses(false, ex.Message);
            }
        }

        public async Task<UserEdit> GetByIdEditAsync(Guid id)
        {
            if (id == Guid.Empty) return null!;

            var user = await context.Users.FindAsync(id);
            if (user is null) return null!;

            var userRole = await context.UserRoles.FindAsync(user.UserRoleId);
            if (userRole != null)
                user.UserRole = userRole;

            UserEdit userEdit = new(user.Id, user.UserRoleId, user.UserDepartmentId, user.FirstName, user.LastName,
                                    user.Patronymic, user.Specialization, user.Email, user.IsActive);

            return userEdit;
        }
    }
}
