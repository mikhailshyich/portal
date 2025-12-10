using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Warehouses;
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

        public async Task<Hardware> GetByIdAsync(Guid id)
        {
            var hardwares = await httpClient.GetAsync($"{BaseURI}/{id}");
            var response = await hardwares.Content.ReadFromJsonAsync<Hardware>();
            return response!;
        }

        public async Task<List<Hardware>> GetByUserIdAsync(Guid userId)
        {
            var hardwares = await httpClient.GetAsync($"{BaseURI}/get/{userId}");
            var response = await hardwares.Content.ReadFromJsonAsync<List<Hardware>>();
            return response!;
        }

        public async Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport)
        {
            var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/import", hardwareImport);
            var response = await hardware.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public async Task<CustomGeneralResponses> MarkAllHardware(List<Guid> hardwareId)
        {
            var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}/marking", hardwareId);
            var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomGeneralResponses> MoveToUserAsync(List<Guid> hardwaresID, Guid? userID, Guid? userWarehouseID)
        {
            var hardwares = await httpClient.PostAsJsonAsync($"{BaseURI}/move/{userID}/{userWarehouseID}", hardwaresID);
            var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }
    }
}
