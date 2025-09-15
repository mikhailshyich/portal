using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IUserWarehouse
    {
        Task<CustomGeneralResponses> AddAsync(UserWarehouseDTO request);
        Task<CustomGeneralResponses> UpdateAsync(UserWarehouse request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<UserWarehouse>> GetAllAsync();
        Task<List<UserWarehouse>> GetAllByUserIdAsync(Guid id);
    }
}
