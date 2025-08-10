using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDomain userInterface;

        public UserService(IUserDomain userInterface)
        {
            this.userInterface = userInterface;
        }

        public async Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request)
        {
            return await userInterface.AddRoleAsync(request);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await userInterface.GetAllAsync();
        }

        public async Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            return await userInterface.GetByIdAsync(id);
        }

        public async Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            return await userInterface.LoginAsync(request);
        }

        public async Task<CustomAuthResponses> RegisterAsync(RegisterDTO request)
        {
            return await userInterface.RegisterAsync(request);
        }
    }
}
