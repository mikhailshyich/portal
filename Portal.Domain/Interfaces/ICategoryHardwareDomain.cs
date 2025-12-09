using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface ICategoryHardwareDomain
    {
        Task<CustomGeneralResponses> AddAsync(CategoryHardwareDTO request);
        Task<CustomGeneralResponses> UpdateAsync(CategoryHardware request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<CategoryHardware>> GetAllAsync();
        Task<CategoryHardware> GetByIdAsync(Guid id);
    }
}
