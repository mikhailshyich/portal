using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public class CategoryHardwareService : ICategoryHardware
    {
        private readonly ICategoryHardwareDomain categoryHardwareDomain;

        public CategoryHardwareService(ICategoryHardwareDomain categoryHardwareDomain)
        {
            this.categoryHardwareDomain = categoryHardwareDomain;
        }

        public async Task<CustomGeneralResponses> AddAsync(CategoryHardwareDTO request)
        {
            return await categoryHardwareDomain.AddAsync(request);
        }

        public async Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            return await categoryHardwareDomain.DeleteAsync(id);
        }

        public async Task<List<CategoryHardware>> GetAllAsync()
        {
            return await categoryHardwareDomain.GetAllAsync();
        }

        public async Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            return await categoryHardwareDomain.GetByIdAsync(id);
        }

        public async Task<CustomGeneralResponses> UpdateAsync(CategoryHardware request)
        {
            return await categoryHardwareDomain.UpdateAsync(request);
        }
    }
}
