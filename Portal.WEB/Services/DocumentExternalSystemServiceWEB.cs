using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;
using System.Net.Http.Headers;

namespace Portal.WEB.Services
{
    public class DocumentExternalSystemServiceWEB : IDocumentExternalSystemServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedLocalStorage localStorage;
        private readonly string BaseURI = "/api/Documents";
        public DocumentExternalSystemServiceWEB(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<CustomGeneralResponses> AddAsync(DocumentExternalSystemDTO request)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var document = await httpClient.PostAsJsonAsync($"{BaseURI}", request);
                var response = await document.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DocumentExternalSystem>> GetAllAsync()
        {
            bool status = await GetAddToken();
            if (status)
            {
                var documents = await httpClient.GetAsync($"{BaseURI}");
                var response = await documents.Content.ReadFromJsonAsync<List<DocumentExternalSystem>>();
                return response!;
            }
            return null!;
        }

        public async Task<DocumentExternalSystem> GetByIdAsync(Guid id)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var document = await httpClient.GetAsync($"{BaseURI}/{id}");
                var response = await document.Content.ReadFromJsonAsync<DocumentExternalSystem>();
                return response!;
            }
            return null!;
        }

        public Task<CustomGeneralResponses> UpdateAsync(DocumentExternalSystem request)
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
