using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IHardwareDomain
    {
        Task<CustomGeneralResponses> AddAsync(HardwareDTO request);
        Task<List<Hardware>> GetAllAsync();
        Task<CustomGeneralResponses> MoveToUserAsync(List<Guid> hardwaresID, Guid userID, Guid userWarehouseID);
        Task<string> GenerateQR(List<Guid>? idList);
        Task<string> GenerateLabel(List<Guid>? idList);
    }
}
