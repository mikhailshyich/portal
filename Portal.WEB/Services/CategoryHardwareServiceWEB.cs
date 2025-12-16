using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;
using System.Net.Http.Headers;

namespace Portal.WEB.Services
{
    public class CategoryHardwareServiceWEB : ICategoryHardwareServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedLocalStorage localStorage;
        private readonly string BaseURI = "/api/Categories";

        public CategoryHardwareServiceWEB(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<CustomGeneralResponses> AddAsync(CategoryHardwareDTO request)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var category = await httpClient.PostAsJsonAsync($"{BaseURI}", request);
                var response = await category.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryHardware>> GetAllAsync()
        {
            bool status = await GetAddToken();
            if (status)
            {
                var categories = await httpClient.GetAsync($"{BaseURI}");
                var response = await categories.Content.ReadFromJsonAsync<List<CategoryHardware>>();
                return response!;
            }
            return null!;
        }

        public async Task<CategoryHardware> GetByIdAsync(Guid id)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var categorie = await httpClient.GetAsync($"{BaseURI}/{id}");
                var response = await categorie.Content.ReadFromJsonAsync<CategoryHardware>();
                return response!;
            }
            return null!;
        }

        public Task<CustomGeneralResponses> UpdateAsync(CategoryHardware request)
        {
            throw new NotImplementedException();
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
