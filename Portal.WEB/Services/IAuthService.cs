using Portal.Domain.DTOs;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public interface IAuthService
    {
        Task<CustomAuthResponses> RegisterAsync(RegisterDTO request);
        Task<CustomAuthResponses> LoginAsync(LoginDTO request);
    }
}
