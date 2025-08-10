using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class UserWarehouseRepositiry : IUserWarehouseDomain
    {
        private readonly PortalDbContext context;

        public UserWarehouseRepositiry(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddAsync(UserWarehouseDTO request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var result = await context.UserWarehouses.AnyAsync(r => r.Title == request.Title);
            if (result) return new CustomGeneralResponses(false, "Такой склад у пользователя уже существует.");

            var userWarehouse = new UserWarehouse()
            {
                UserId = request.UserId,
                Title = request.Title
            };

            context.UserWarehouses.Add(userWarehouse);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Склад для пользователя успешно создан.", userWarehouse);
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserWarehouse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> UpdateAsync(UserWarehouse request)
        {
            throw new NotImplementedException();
        }
    }
}
