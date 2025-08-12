using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;
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

        public async Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            if (request is null)
                return new CustomAuthResponses(false, $"Пользователь равен null.");

            var userDB = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (userDB is null)
                return new CustomAuthResponses(false, $"Проверьте введённые данные.");

            if (new PasswordHasher<User>().VerifyHashedPassword(userDB, userDB.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return new CustomAuthResponses(false, "Проверьте введённые данные.");

            var userRole = await context.UserRoles.FirstOrDefaultAsync(r => r.Id == userDB.UserRoleId);

            userDB.UserRole = userRole;

            var jwtToken = CreateToken(userDB);

            await GenerateAndSaveTokensAsync(userDB, jwtToken);

            return new CustomAuthResponses(true, $"Пользователь {userDB.Username} успешно авторизован.", jwtToken);
        }

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

        private async Task GenerateAndSaveTokensAsync(User user, string jwtToken)
        {
            var userTokens = await context.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id);

            if (userTokens != null)
            {
                userTokens.RefreshToken = GenerateRefreshToken();
                userTokens.DateTimeExpiredToken = DateTime.Now.AddDays(7);
            }
            else
            {
                var refreshToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = GenerateRefreshToken(),
                    DateTimeExpiredToken = DateTime.Now.AddDays(7)
                };
                context.UserTokens.Add(refreshToken);
            }

            await context.SaveChangesAsync();
        }

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
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request)
        {
            if(request is null)
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

        public async Task<List<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return null!;

            var user = await context.Users.FindAsync(id);
            if (user is null) return null!;

            var userRole = await context.UserRoles.FindAsync(user.UserRoleId);
            if (userRole != null)
                user.UserRole = userRole;

            var userDepartment = await context.UserDepartments.FindAsync(user.UserDepartmentId);
            if (userDepartment != null)
                user.UserDepartment = userDepartment;

            var userWarehouse = context.UserWarehouses.Where(w => w.UserId == user.Id).ToList();
            if (userWarehouse != null)
                user.UserWarehouses = userWarehouse;

            return user;
        }

        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            return await context.UserRoles.ToListAsync();
        }
    }
}
