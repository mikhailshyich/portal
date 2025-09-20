using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public class UserDepartmentService : IUserDepartment
    {
        private readonly IUserDepartmentDomain userDepartment;

        public UserDepartmentService(IUserDepartmentDomain userDepartment)
        {
            this.userDepartment = userDepartment;
        }

        public async Task<CustomGeneralResponses> AddAsync(UserDepartmentDTO request)
        {
            return await userDepartment.AddAsync(request);
        }

        public async Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            return await userDepartment.DeleteAsync(id);
        }

        public async Task<List<UserDepartment>> GetAllAsync()
        {
            return await userDepartment.GetAllAsync();
        }

        public async Task<UserDepartment> GetByIdAsync(Guid id)
        {
            return await userDepartment.GetByIdAsync(id);
        }

        public Task<CustomGeneralResponses> UpdateAsync(UserDepartment request)
        {
            return userDepartment.UpdateAsync(request);
        }
    }
}
