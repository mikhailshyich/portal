using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IUserDepartmentDomain
    {
        Task<CustomGeneralResponses> AddAsync(UserDepartmentDTO request);
        Task<CustomGeneralResponses> UpdateAsync(UserDepartmentUpdateDTO request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<UserDepartment>> GetAllAsync();
        Task<UserDepartment> GetByIdAsync(Guid id);

    }
}
