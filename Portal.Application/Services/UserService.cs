using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserApp userInterface;

        public UserService(IUserApp userInterface)
        {
            this.userInterface = userInterface;
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
            return await userInterface.LoginAsync(request);
        }

        public async Task<CustomResponses> RegisterAsync(RegisterDTO request)
        {
            return await userInterface.RegisterAsync(request);
        }
    }
}
