using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface ICategoryHardware
    {
        Task<CustomGeneralResponses> AddAsync(CategoryHardwareDTO request);
        Task<CustomGeneralResponses> UpdateAsync(CategoryHardware request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<CategoryHardware>> GetAllAsync();
        Task<CategoryHardware> GetByIdAsync(Guid id);
    }
}
