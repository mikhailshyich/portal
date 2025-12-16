using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;
using System.Net.Http.Headers;

namespace Portal.WEB.Services
{
    public class MainWarehouseServiceWEB : IMainWarehouseServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedLocalStorage localStorage;
        private readonly string BaseURI = "/api/MainWarehouses";
        public MainWarehouseServiceWEB(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<CustomGeneralResponses> AddAsync(MainWarehouseDTO request)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var mainWarehouse = await httpClient.PostAsJsonAsync($"{BaseURI}", request);
                var response = await mainWarehouse.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public Task<MainWarehouse> EditGameAsync(MainWarehouse request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MainWarehouse>> GetAllAsync()
        {
            bool status = await GetAddToken();
            if (status)
            {
                var mainWarehouse = await httpClient.GetAsync($"{BaseURI}");
                var response = await mainWarehouse.Content.ReadFromJsonAsync<List<MainWarehouse>>();
                return response!;
            }
            return null!;
            
        }

        public async Task<MainWarehouse> GetByIdAsync(Guid id)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var mainWarehouse = await httpClient.GetAsync($"{BaseURI}/{id}");
                var response = await mainWarehouse.Content.ReadFromJsonAsync<MainWarehouse>();
                return response!;
            }
            return null!;
        }

        private async Task<bool> GetAddToken()
        {
            var token = await localStorage.GetAsync<string>("authToken");
            if (token.Value != string.Empty)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                return true;
            }
            return false;
        }
    }
}
