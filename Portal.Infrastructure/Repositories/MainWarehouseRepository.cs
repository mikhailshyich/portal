using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
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

        public async Task<CustomGeneralResponses> AddAsync(MainWarehouseDTO request)
        {
            if (request is null)
                return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var mainWarehouse = new MainWarehouse()
            {
                UserDepartmentId = request.UserDepartmentId,
                Title = request.Title,
                Description = request.Description,
            };

            await context.MainWarehouses.AddAsync(mainWarehouse);
            await context.SaveChangesAsync();

            return new CustomGeneralResponses(true, "Склад успешно добавлен.", mainWarehouse);
        }

        public async Task<List<MainWarehouse>> GetAllAsync()
        {
            var mainWarehouses = await context.MainWarehouses.ToListAsync();
            foreach(var mainWarehouse in mainWarehouses)
            {
                var userDepartment = await context.UserDepartments.FindAsync(mainWarehouse.UserDepartmentId);
                if(userDepartment != null)
                    mainWarehouse.UserDepartment = userDepartment;
            }
            return mainWarehouses;
        }

        public async Task<MainWarehouse> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return null!;

            var mainWarehouse = await context.MainWarehouses.FindAsync(id);
            if (mainWarehouse is null) return null!;

            var department = await context.UserDepartments.FindAsync(mainWarehouse.UserDepartmentId);
            if (department != null)
            {
                mainWarehouse.UserDepartment = department;
            }

            var hardwares = context.Hardwares.Where(m => m.MainWarehouseId == mainWarehouse.Id & m.IsActive == true).ToList();
            if (hardwares != null)
            {
                mainWarehouse.Hardwares = hardwares;
            }

            foreach (var hardware in mainWarehouse.Hardwares)
            {
                var category = await context.CategoriesHardware.FindAsync(hardware.CategoryHardwareId);
                var document = await context.DocumentsExternalSystem.FindAsync(hardware.DocumentExternalSystemId);
                if (hardware.UserId != null)
                {
                    var user = await context.Users.FindAsync(hardware.UserId);
                    hardware.User = user;
                }
                hardware.CategoryHardware = category;
                hardware.DocumentExternalSystem = document;
            }

            return mainWarehouse;
        }
    }
}
