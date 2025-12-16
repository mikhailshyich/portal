using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class UserDepartmentServiceWEB : IUserDepartmentServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly string BaseURI = "api/Departments";

        public UserDepartmentServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomGeneralResponses> AddAsync(UserDepartmentDTO request)
        {
            var departmen = await httpClient.PostAsJsonAsync($"{BaseURI}", request);
            var response = await departmen.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDepartment>> GetAllAsync()
        {
            var departmens = await httpClient.GetAsync($"{BaseURI}");
            var response = await departmens.Content.ReadFromJsonAsync<List<UserDepartment>>();
            return response!;
        }

        public async Task<UserDepartment> GetByIdAsync(Guid id)
        {
            var department = await httpClient.GetAsync($"{BaseURI}/{id}");
            var response = await department.Content.ReadFromJsonAsync<UserDepartment>();
            return response!;
        }

        public Task<CustomGeneralResponses> UpdateAsync(UserDepartmentUpdateDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
