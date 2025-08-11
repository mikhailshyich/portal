using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;
using System.Net.Http;

namespace Portal.WEB.Services
{
    public class UserDepartmentServiceWEB : IUserDepartmentServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly string BaseURI = "api/UserDepartment";

        public UserDepartmentServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<CustomGeneralResponses> AddAsync(UserDepartmentDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDepartment>> GetAllAsync()
        {
            var departmens = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await departmens.Content.ReadFromJsonAsync<List<UserDepartment>>();
            return response!;
        }

        public Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> UpdateAsync(UserDepartment request)
        {
            throw new NotImplementedException();
        }
    }
}
