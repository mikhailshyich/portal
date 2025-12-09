using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IUserService
    {
        Task<List<UserView>> GetAllAsync();
        Task<UserView> GetByIdAsync(Guid id);
        //Task<User> GetByEmailAsync(string email);
        Task<UserView> GetByUsernameAsync(string username);
        Task<CustomGeneralResponses> RegisterAsync(RegisterDTO request);
        Task<CustomAuthResponses> LoginAsync(LoginDTO request);
        Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request);
        Task<List<UserRole>> GetAllUserRolesAsync();
        Task<CustomGeneralResponses> SyncUsersAsync();
        Task<UserEdit> GetByIdEditAsync(Guid id);
        Task<CustomGeneralResponses> EditUserAsync(UserView request);
        //Task CheckTokens(LoginDTO request);
    }
}
