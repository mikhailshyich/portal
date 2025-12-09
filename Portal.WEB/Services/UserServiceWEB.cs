using DocumentFormat.OpenXml.Office2016.Excel;
using Portal.Domain.DTOs;
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

        public async Task<List<UserView>> GetAllAsync()
        {
            var users = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await users.Content.ReadFromJsonAsync<List<UserView>>();
            return response!;
        }

        public async Task<UserView> GetByIdAsync(Guid id)
        {
            var user = await httpClient.GetAsync($"{BaseURI}/{id}");
            var response = await user.Content.ReadFromJsonAsync<UserView>();
            return response!;
        }

        public async Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            var user = await httpClient.PostAsJsonAsync($"{BaseURI}/login", request);
            var response = await user.Content.ReadFromJsonAsync<CustomAuthResponses>();
            return response!;
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

        public async Task<CustomGeneralResponses> SyncUsersAsync()
        {
            var users = await httpClient.PostAsync($"{BaseURI}/sync", null);
            var response = await users.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public async Task<UserView> GetByUsernameAsync(string username)
        {
            var user = await httpClient.GetAsync($"{BaseURI}/username/{username}");
            var response = await user.Content.ReadFromJsonAsync<UserView>();
            return response!;
        }

        public async Task<CustomGeneralResponses> EditUserAsync(UserView request)
        {
            var user = await httpClient.PutAsJsonAsync($"{BaseURI}/edit", request);
            var response = await user.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public async Task<UserEdit> GetByIdEditAsync(Guid id)
        {
            var user = await httpClient.GetAsync($"{BaseURI}/{id}");
            var response = await user.Content.ReadFromJsonAsync<UserEdit>();
            return response!;
        }
    }
}
