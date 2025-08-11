using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class UserServiceWEB : IUserServiceWEB
    {
        private readonly HttpClient httpClient;

        public UserServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        private readonly string BaseURI = "api/User";

        public Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await users.Content.ReadFromJsonAsync<List<User>>();
            return response!;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await httpClient.GetAsync($"{BaseURI}/{id}");
            var response = await user.Content.ReadFromJsonAsync<User>();
            return response!;
        }

        public Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomGeneralResponses> RegisterAsync(RegisterDTO request)
        {
            var user = await httpClient.PostAsJsonAsync($"{BaseURI}/register", request);
            var response = await user.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            var roles = await httpClient.GetAsync($"{BaseURI}/roles");
            var response = await roles.Content.ReadFromJsonAsync<List<UserRole>>();
            return response!;
        }
    }
}
