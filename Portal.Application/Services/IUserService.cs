using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<CustomGeneralResponses> GetByIdAsync(Guid id);
        //Task<User> GetByEmailAsync(string email);
        //Task<User> GetByUsernameAsync(string username);
        Task<CustomAuthResponses> RegisterAsync(RegisterDTO request);
        Task<CustomAuthResponses> LoginAsync(LoginDTO request);
        Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request);
        //Task CheckTokens(LoginDTO request);
    }
}
