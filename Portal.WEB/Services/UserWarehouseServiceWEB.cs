using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class UserWarehouseServiceWEB : IUserWarehouseServiceWEB
    {
        private readonly HttpClient httpClient;

        public UserWarehouseServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private readonly string BaseURI = "api/UserWarehouse";

        public async Task<CustomGeneralResponses> AddAsync(UserWarehouseDTO request)
        {
            var warehouse = await httpClient.PostAsJsonAsync($"{BaseURI}/add", request);
            var response = await warehouse.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public Task<CustomGeneralResponses> UpdateAsync(UserWarehouse request)
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserWarehouse>> GetAllAsync()
        {
            var warehouses = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await warehouses.Content.ReadFromJsonAsync<List<UserWarehouse>>();
            return response!;
        }

        public async Task<List<UserWarehouse>> GetAllByUserIdAsync(Guid id)
        {
            var warehouses = await httpClient.GetAsync($"{BaseURI}/get/user/{id}");
            var response = await warehouses.Content.ReadFromJsonAsync<List<UserWarehouse>>();
            return response!;
        }
    }
}
