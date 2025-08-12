using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IMainWarehouseDomain
    {
        Task<List<MainWarehouse>> GetAllAsync();
        Task<CustomGeneralResponses> AddAsync(MainWarehouseDTO request);
        Task<MainWarehouse> GetByIdAsync(Guid id);
        Task<MainWarehouse> EditGameAsync(MainWarehouse request);
    }
}
