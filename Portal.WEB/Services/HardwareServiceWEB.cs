using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;
using System.Net.Http.Headers;

namespace Portal.WEB.Services
{
    public class HardwareServiceWEB : IHardwareServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedLocalStorage localStorage;
        private readonly string BaseURI = "/api/Hardwares";

        public HardwareServiceWEB(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}", request);
                var response = await hardware.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public async Task<string> GenerateLabel(List<Guid>? idList)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/label", idList);
                var response = await hardware.Content.ReadAsStringAsync();
                return response!;
            }
            return null!;
        }

        public async Task<string> GenerateQR(List<Guid>? idList)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/qr", idList);
                var response = await hardware.Content.ReadAsStringAsync();
                return response!;
            }
            return null!;
        }

        public async Task<List<Hardware>> GetAllAsync()
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.GetAsync($"{BaseURI}");
                var response = await hardwares.Content.ReadFromJsonAsync<List<Hardware>>();
                return response!;
            }
            return null!;
        }

        public async Task<Hardware> GetByIdAsync(Guid id)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.GetAsync($"{BaseURI}/{id}");
                var response = await hardwares.Content.ReadFromJsonAsync<Hardware>();
                return response!;
            }
            return null!;
        }

        public async Task<List<Hardware>> GetByUserIdAsync(Guid userId)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.GetAsync($"{BaseURI}/users/{userId}");
                var response = await hardwares.Content.ReadFromJsonAsync<List<Hardware>>();
                return response!;
            }
            return null!;
        }

        public async Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/import", hardwareImport);
                var response = await hardware.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public async Task<CustomGeneralResponses> MarkAllHardware(List<Guid> hardwareId)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}/marking", hardwareId);
                var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}/move", moveDTO);
                var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public async Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}/return", returnDTO);
                var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        public async Task<Hardware> UpdateAsync(HardwareUpdateDTO updateDTO)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}", updateDTO);
                var response = await hardwares.Content.ReadFromJsonAsync<Hardware>();
                return response!;
            }
            return null!;
        }

        public async Task<CustomGeneralResponses> WriteOff(HardwareWriteOffDTO writeOffDTO)
        {
            bool status = await GetAddToken();
            if (status)
            {
                var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}/writeoff", writeOffDTO);
                var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
                return response!;
            }
            return null!;
        }

        private async Task<bool> GetAddToken()
        {
            var token = await localStorage.GetAsync<string>("authToken");
            if(token.Value != string.Empty)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                return true;
            }
            return false;
        }
    }
}
