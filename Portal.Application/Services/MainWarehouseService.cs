using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public class MainWarehouseService : IMainWarehouse
    {
        private readonly IMainWarehouseDomain mainWarehouseDomainInterface;
        public MainWarehouseService(IMainWarehouseDomain mainWarehouseDomainInterface)
        {
            this.mainWarehouseDomainInterface = mainWarehouseDomainInterface;
        }

        public async Task<CustomGeneralResponses> AddAsync(MainWarehouseDTO request)
        {
            return await mainWarehouseDomainInterface.AddAsync(request);
        }

        public Task<MainWarehouse> EditGameAsync(MainWarehouse request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MainWarehouse>> GetAllAsync()
        {
            return await mainWarehouseDomainInterface.GetAllAsync();
        }

        public async Task<MainWarehouse> GetByIdAsync(Guid id)
        {
            return await mainWarehouseDomainInterface.GetByIdAsync(id);
        }
    }
}
