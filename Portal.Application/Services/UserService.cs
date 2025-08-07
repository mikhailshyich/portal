using Portal.Domain.DTOs;
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

        public async Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request)
        {
            return await userInterface.AddRoleAsync(request);
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
