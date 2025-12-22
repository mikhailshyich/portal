using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public interface IUserWarehouseServiceWEB
    {
        Task<CustomGeneralResponses> AddAsync(UserWarehouseDTO request);
        Task<CustomGeneralResponses> UpdateAsync(UserWarehouse request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<UserWarehouse>> GetAllAsync();
        Task<List<UserWarehouse>> GetAllByUserIdAsync(Guid id);
        Task<UserWarehouse> GetByIdAsync(Guid id);
    }
}
