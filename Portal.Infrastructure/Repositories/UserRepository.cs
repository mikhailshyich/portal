using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

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

        public async Task<User> LoginAsync(LoginDTO request)
        {
            if (request is null)
                return null!;

            var userDB = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (userDB is null)
                return null!;

            if(new PasswordHasher<User>().VerifyHashedPassword(userDB, userDB.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return null!;

            return userDB;
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
    }
}
