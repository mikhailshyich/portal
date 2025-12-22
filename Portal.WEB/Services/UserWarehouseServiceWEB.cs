using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;
using System.Net.Http.Headers;

namespace Portal.WEB.Services
{
    public class UserWarehouseServiceWEB : IUserWarehouseServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedLocalStorage localStorage;

        public UserWarehouseServiceWEB(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        private readonly string BaseURI = "api/UsersWarehouses";

        public async Task<CustomGeneralResponses> AddAsync(UserWarehouseDTO request)
        {
            var warehouse = await httpClient.PostAsJsonAsync($"{BaseURI}", request);
            var response = await warehouse.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public Task<CustomGeneralResponses> UpdateAsync(UserWarehouse request)
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserWarehouse>> GetAllAsync()
        {
            var warehouses = await httpClient.GetAsync($"{BaseURI}");
            var response = await warehouses.Content.ReadFromJsonAsync<List<UserWarehouse>>();
            return response!;
        }

        public async Task<List<UserWarehouse>> GetAllByUserIdAsync(Guid id)
        {
            var warehouses = await httpClient.GetAsync($"{BaseURI}/users/{id}");
            var response = await warehouses.Content.ReadFromJsonAsync<List<UserWarehouse>>();
            return response!;
        }

        public async Task<UserWarehouse> GetByIdAsync(Guid id)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var warehouse = await httpClient.GetAsync($"{BaseURI}/{id}");
                var response = await warehouse.Content.ReadFromJsonAsync<UserWarehouse>();
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
