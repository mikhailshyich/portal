using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IHardwareDomain
    {
        Task<CustomGeneralResponses> AddAsync(HardwareDTO request);
        Task<List<Hardware>> GetAllAsync();
    }
}
