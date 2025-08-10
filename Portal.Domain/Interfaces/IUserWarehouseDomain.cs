using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IUserWarehouseDomain
    {
        Task<CustomGeneralResponses> AddAsync(UserWarehouseDTO request);
        Task<CustomGeneralResponses> UpdateAsync(UserWarehouse request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<UserWarehouse>> GetAllAsync();
        Task<CustomGeneralResponses> GetByIdAsync(Guid id);
    }
}
