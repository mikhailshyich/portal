using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class CategoryHardwareServiceWEB : ICategoryHardwareServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly string BaseURI = "/api/CategoryHardware";

        public CategoryHardwareServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomGeneralResponses> AddAsync(CategoryHardwareDTO request)
        {
            var category = await httpClient.PostAsJsonAsync($"{BaseURI}/add", request);
            var response = await category.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryHardware>> GetAllAsync()
        {
            var categories = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await categories.Content.ReadFromJsonAsync<List<CategoryHardware>>();
            return response!;
        }

        public Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> UpdateAsync(CategoryHardware request)
        {
            throw new NotImplementedException();
        }
    }
}
