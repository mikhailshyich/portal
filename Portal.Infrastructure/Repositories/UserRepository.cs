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
using System.Text.Json;

namespace Portal.Infrastructure.Repositories
{
    public class UserRepository : IUserApp
    {
        private readonly PortalDbContext context;

        public UserRepository(PortalDbContext context)
        {
            this.context = context;
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

        public async Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            if (request is null)
                return new CustomAuthResponses(false, $"Пользователь равен null.");

            var userDB = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (userDB is null)
                return new CustomAuthResponses(false, $"Проверьте введённые данные.");

            if (new PasswordHasher<User>().VerifyHashedPassword(userDB, userDB.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return new CustomAuthResponses(false, "Проверьте введённые данные.");

            var jwtToken = CreateToken(userDB);

            await GenerateAndSaveTokensAsync(userDB, jwtToken);

            return new CustomAuthResponses(true, $"Пользователь {userDB.Username} успешно авторизован.", jwtToken);
        }

        public Task CheckTokens()
        {
            //сделать метод для проверки токенов
            throw new NotImplementedException();
        }

        public async Task<CustomAuthResponses> RegisterAsync(RegisterDTO request)
        {
            if (request is null)
                return new CustomAuthResponses(false, $"Проверьте введённые данные.");

            var userDB = await context.Users.AnyAsync(u => u.Username == request.Username);
            if (userDB)
                return new CustomAuthResponses(false, $"Пользователь *{request.Username}* уже зарегистрирован.");

            var newUser = new User();

            newUser.Username = request.Username;
            newUser.Email = request.Email;
            newUser.PasswordHash = new PasswordHasher<User>().HashPassword(newUser, request.Password);

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return new CustomAuthResponses(true, $"Пользователь *{request.Username}* успешно зарегистрирован.");
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
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("O]N3SEuo{WnvC~<rcw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDSEuo{WnvC~<rcw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@oh–zw^baX}cw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@f0zdl@oh–zw^baX}cw:8qq%ZniMbQ$_ER–.0,%tY@R?OIDf0zdl@oh–zw^baX}"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: "Turov Dairy Industrial Complex",
                audience: "Employees Turov Dairy Industrial Complex",
                claims: claimsList,
                expires: DateTime.Now.AddMinutes(1),
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

        public Task CheckTokens(LoginDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
