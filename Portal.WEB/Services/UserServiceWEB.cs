using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;
using System.Net.Http.Headers;

namespace Portal.WEB.Services
{
    public class UserServiceWEB : IUserServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedLocalStorage localStorage;
        private readonly string BaseURI = "api/Users";

        public UserServiceWEB(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public Task<CustomGeneralResponses> AddRoleAsync(UserRoleDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserView>> GetAllAsync()
        {
            bool status = await GetAddToken();
            if (status)
            {
                var users = await httpClient.GetAsync($"{BaseURI}");
                var response = await users.Content.ReadFromJsonAsync<List<UserView>>();
                return response!;
            }
            return null!;
        }

        public async Task<UserView> GetByIdAsync(Guid id)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var user = await httpClient.GetAsync($"{BaseURI}/{id}");
                var response = await user.Content.ReadFromJsonAsync<UserView>();
                return response!;
            }
            return null!;
        }

        public async Task<CustomAuthResponses> LoginAsync(LoginDTO request)
        {
            var user = await httpClient.PostAsJsonAsync($"{BaseURI}/login", request);
            var response = await user.Content.ReadFromJsonAsync<CustomAuthResponses>();
            return response!;
        }

        public async Task<CustomGeneralResponses> RegisterAsync(RegisterDTO request)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var user = await httpClient.PostAsJsonAsync($"{BaseURI}/register", request);
                var response = await user.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            bool status = await GetAddToken();
            if (status)
            {
                var roles = await httpClient.GetAsync($"{BaseURI}/roles");
                var response = await roles.Content.ReadFromJsonAsync<List<UserRole>>();
                return response!;
            }
            return null!;
        }

        public async Task<CustomGeneralResponses> SyncUsersAsync()
        {
            bool status = await GetAddToken();
            if (status)
            {
                var users = await httpClient.PostAsync($"{BaseURI}/sync", null);
                var response = await users.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public async Task<UserView> GetByUsernameAsync(string username)
        {
            try
            {
                bool status = await GetAddToken();
                if (status)
                {
                    var user = await httpClient.GetAsync($"{BaseURI}/usernames/{username}");
                    var response = await user.Content.ReadFromJsonAsync<UserView>();
                    return response!;
                }
                return null!;
                
            }
            catch(Exception ex) { return null!; }
        }

        public async Task<CustomGeneralResponses> EditUserAsync(UserEdit request)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var user = await httpClient.PutAsJsonAsync($"{BaseURI}/edit", request);
                var response = await user.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        private async Task<bool> GetAddToken()
        {
            try
            {
                var token = await localStorage.GetAsync<string>("authToken");
                if (token.Value != string.Empty)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
    }
}
