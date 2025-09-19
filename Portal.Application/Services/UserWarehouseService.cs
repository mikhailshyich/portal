using Portal.Domain.DTOs;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class UserWarehouseService : IUserWarehouse
    {
        private readonly IUserWarehouseDomain userWarehouseDomain;

        public UserWarehouseService(IUserWarehouseDomain userWarehouseDomain)
        {
            this.userWarehouseDomain = userWarehouseDomain;
        }

        public async Task<CustomGeneralResponses> AddAsync(UserWarehouseDTO request)
        {
            return await userWarehouseDomain.AddAsync(request);
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserWarehouse>> GetAllAsync()
        {
            return await userWarehouseDomain.GetAllAsync();
        }

        public async Task<List<UserWarehouse>> GetAllByUserIdAsync(Guid id)
        {
            return await userWarehouseDomain.GetAllByUserIdAsync(id);
        }

        public Task<CustomGeneralResponses> UpdateAsync(UserWarehouse request)
        {
            throw new NotImplementedException();
        }
    }
}
