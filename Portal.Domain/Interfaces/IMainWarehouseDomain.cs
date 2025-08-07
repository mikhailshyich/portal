using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;

namespace Portal.Domain.Interfaces
{
    public interface IMainWarehouseDomain
    {
        Task<List<MainWarehouse>> GetAllAsync();
        Task<MainWarehouse> AddAsync(MainWarehouseDTO request);
        Task<MainWarehouse> GetByIdAsync(Guid id);
        Task<MainWarehouse> EditGameAsync(MainWarehouse request);
    }
}
