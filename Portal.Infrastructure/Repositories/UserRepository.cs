using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Portal.Infrastructure.Repositories
{
    public class UserRepository : IUserApp
    {
        private readonly PortalDbContext context;
        private readonly IConfiguration configuration;

        public UserRepository(PortalDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponses> LoginAsync(LoginDTO request)
        {
            if (request is null)
                return new CustomResponses(false, $"Пользователь равен null.");

            var userDB = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (userDB is null)
                return new CustomResponses(false, $"Проверьте введённые данные.");

            if (new PasswordHasher<User>().VerifyHashedPassword(userDB, userDB.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return new CustomResponses(false, "Проверьте введённые данные.");

            await GenerateAndSaveTokensAsync(userDB);

            return new CustomResponses(true, $"Пользователь {userDB.Username} успешно авторизован.");
        }

        public Task CheckTokens()
        {
            //сделать метод для проверки токенов
            throw new NotImplementedException();
        }

        public async Task<CustomResponses> RegisterAsync(RegisterDTO request)
        {
            if (request is null)
                return new CustomResponses(false, $"Проверьте введённые данные.");

            var userDB = await context.Users.AnyAsync(u => u.Username == request.Username);
            if (userDB)
                return new CustomResponses(false, $"Пользователь *{request.Username}* уже зарегистрирован.");

            var newUser = new User();

            newUser.Username = request.Username;
            newUser.Email = request.Email;
            newUser.PasswordHash = new PasswordHasher<User>().HashPassword(newUser, request.Password);
            newUser.Role = request.Role;

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return new CustomResponses(true, $"Пользователь *{request.Username}* успешно зарегистрирован.");
        }

        private async Task GenerateAndSaveTokensAsync(User user)
        {
            var refTokenDB = await context.UserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id);

            if (refTokenDB != null)
            {
                refTokenDB.RefToken = GenerateRefreshToken();
                refTokenDB.DateTimeExpiredRef = DateTime.Now.AddDays(7);
            }
            else
            {
                var refreshToken = new UserToken()
                {
                    UserId = user.Id,
                    JWTToken = CreateToken(user),
                    DateTimeExpiredJWT = DateTime.Now.AddDays(1),
                    RefToken = GenerateRefreshToken(),
                    DateTimeExpiredRef = DateTime.Now.AddDays(7)
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
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("O]N3SEuo{WnvC~<rcw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDSEuo{WnvC~<rcw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@oh–zw^baX}cw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@f0zdl@oh–zw^baX}cw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@oh–zw^baX}"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: "Turov Dairy Industrial Complex",
                audience: "Employees Turov Dairy Industrial Complex",
                claims: claimsList,
                expires: DateTime.Now.AddDays(1),
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
    }
}
