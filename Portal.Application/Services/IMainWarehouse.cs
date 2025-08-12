using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IMainWarehouse
    {
        Task<List<MainWarehouse>> GetAllAsync();
        Task<CustomGeneralResponses> AddAsync(MainWarehouseDTO request);
        Task<MainWarehouse> GetByIdAsync(Guid id);
        Task<MainWarehouse> EditGameAsync(MainWarehouse request);
    }
}
