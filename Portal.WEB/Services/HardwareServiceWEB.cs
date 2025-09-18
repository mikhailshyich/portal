using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class HardwareServiceWEB : IHardwareServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly string BaseURI = "/api/Hardware";

        public HardwareServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/add", request);
            var response = await hardware.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public async Task<string> GenerateLabel(List<Guid>? idList)
        {
            var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/generatelabel", idList);
            var response = await hardware.Content.ReadAsStringAsync();
            return response!;
        }

        public async Task<string> GenerateQR(List<Guid>? idList)
        {
            var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/generateqr", idList);
            var response = await hardware.Content.ReadAsStringAsync();
            return response!;
        }

        public async Task<List<Hardware>> GetAllAsync()
        {
            var hardwares = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await hardwares.Content.ReadFromJsonAsync<List<Hardware>>();
            return response!;
        }

        public async Task<List<Hardware>> GetByUserIdAsync(Guid userId)
        {
            var hardwares = await httpClient.GetAsync($"{BaseURI}/get/{userId}");
            var response = await hardwares.Content.ReadFromJsonAsync<List<Hardware>>();
            return response!;
        }
    }
}
