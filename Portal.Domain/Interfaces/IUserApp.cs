using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IUserApp
    {
        //Task<List<User>> GetAllAsync();
        //Task<User> GetByIdAsync(Guid id);
        //Task<User> GetByEmailAsync(string email);
        //Task<User> GetByUsernameAsync(string username);
        Task<CustomResponses> RegisterAsync(RegisterDTO request);
        Task<CustomResponses> LoginAsync(LoginDTO request);
        Task CheckTokens();
    }
}
