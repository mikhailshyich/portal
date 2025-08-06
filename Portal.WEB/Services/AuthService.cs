using Portal.Domain.DTOs;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class AuthService
    {
        private readonly HttpClient httpClient;

        public AuthService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private readonly string BaseURI = "api/User";

        public Task CheckTokens(LoginDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            var response = await httpClient.PostAsJsonAsync($"{BaseURI}/login", request);
            var result = await response.Content.ReadFromJsonAsync<CustomAuthResponses>();
            return result!;
        }

        public async Task<CustomAuthResponses> RegisterAsync(RegisterDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
