using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public interface ICategoryHardwareServiceWEB
    {
        Task<CustomGeneralResponses> AddAsync(CategoryHardwareDTO request);
        Task<CustomGeneralResponses> UpdateAsync(CategoryHardware request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<CategoryHardware>> GetAllAsync();
        Task<CustomGeneralResponses> GetByIdAsync(Guid id);
    }
}
