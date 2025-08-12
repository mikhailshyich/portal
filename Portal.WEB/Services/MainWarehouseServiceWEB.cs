using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class MainWarehouseServiceWEB : IMainWarehouseServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly string BaseURI = "/api/MainWarehouse";
        public MainWarehouseServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomGeneralResponses> AddAsync(MainWarehouseDTO request)
        {
            var mainWarehouse = await httpClient.PostAsJsonAsync($"{BaseURI}/add", request);
            var response = await mainWarehouse.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public Task<MainWarehouse> EditGameAsync(MainWarehouse request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MainWarehouse>> GetAllAsync()
        {
            var mainWarehouse = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await mainWarehouse.Content.ReadFromJsonAsync<List<MainWarehouse>>();
            return response!;
        }

        public Task<MainWarehouse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
