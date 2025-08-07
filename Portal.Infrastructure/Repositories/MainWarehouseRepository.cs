using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class MainWarehouseRepository : IMainWarehouseDomain
    {
        private readonly PortalDbContext context;

        public MainWarehouseRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<MainWarehouse> AddAsync(MainWarehouseDTO request)
        {
            if (request is null)
                return null!;

            var mainWarehouse = new MainWarehouse()
            {
                UserDepartmentId = request.UserDepartmentId,
                Title = request.Title,
                Description = request.Description,
            };

            await context.MainWarehouses.AddAsync(mainWarehouse);
            await context.SaveChangesAsync();

            return mainWarehouse;
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
