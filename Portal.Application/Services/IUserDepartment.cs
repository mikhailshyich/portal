using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IUserDepartment
    {
        Task<CustomGeneralResponses> AddAsync(UserDepartmentDTO request);
        Task<CustomGeneralResponses> UpdateAsync(UserDepartment request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<UserDepartment>> GetAllAsync();
        Task<CustomGeneralResponses> GetByIdAsync(Guid id);
    }
}
