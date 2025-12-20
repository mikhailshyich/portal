using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IUserDomain
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
        Task<CustomGeneralResponses> EditUserAsync(UserEdit request);
        //Task CheckTokens(LoginDTO request);
    }
}
