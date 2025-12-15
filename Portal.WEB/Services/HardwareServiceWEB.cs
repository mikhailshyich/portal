using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class HardwareServiceWEB : IHardwareServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly string BaseURI = "/api/Hardwares";

        public HardwareServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}", request);
            var response = await hardware.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public async Task<string> GenerateLabel(List<Guid>? idList)
        {
            var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/label", idList);
            var response = await hardware.Content.ReadAsStringAsync();
            return response!;
        }

        public async Task<string> GenerateQR(List<Guid>? idList)
        {
            var hardware = await httpClient.PostAsJsonAsync($"{BaseURI}/qr", idList);
            var response = await hardware.Content.ReadAsStringAsync();
            return response!;
        }

        public async Task<List<Hardware>> GetAllAsync()
        {
            var hardwares = await httpClient.GetAsync($"{BaseURI}");
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
            var hardwares = await httpClient.GetAsync($"{BaseURI}/users/{userId}");
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

        public async Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO)
        {
            var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}/move", moveDTO);
            var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public async Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO)
        {
            var hardwares = await httpClient.PatchAsJsonAsync($"{BaseURI}/return", returnDTO);
            var response = await hardwares.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }
    }
}
