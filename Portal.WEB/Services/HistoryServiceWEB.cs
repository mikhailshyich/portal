using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Entities.History;
using Portal.Domain.Responses;
using System.Net.Http.Headers;

namespace Portal.WEB.Services
{
    public class HistoryServiceWEB : IHistoryServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedLocalStorage localStorage;
        private readonly string BaseURI = "/api/History";

        public HistoryServiceWEB(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<List<History>> GetByHardwareIdAsync(Guid hardwareId)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var history = await httpClient.GetAsync($"{BaseURI}/hardwares/{hardwareId}");
                var response = await history.Content.ReadFromJsonAsync<List<History>>();
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
