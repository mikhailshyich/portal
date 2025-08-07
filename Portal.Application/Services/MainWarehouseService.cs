using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;

namespace Portal.Application.Services
{
    public class MainWarehouseService : IMainWarehouse
    {
        private readonly IMainWarehouseDomain mainWarehouseDomainInterface;
        public MainWarehouseService(IMainWarehouseDomain mainWarehouseDomainInterface)
        {
            this.mainWarehouseDomainInterface = mainWarehouseDomainInterface;
        }

        public async Task<MainWarehouse> AddAsync(MainWarehouseDTO request)
        {
            return await mainWarehouseDomainInterface.AddAsync(request);
        }

        public Task<MainWarehouse> EditGameAsync(MainWarehouse request)
        {
            throw new NotImplementedException();
        }

        public Task<List<MainWarehouse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MainWarehouse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
