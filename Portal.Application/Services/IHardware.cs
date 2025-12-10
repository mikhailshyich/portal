using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IHardware
    {
        Task<CustomGeneralResponses> AddAsync(HardwareDTO request);
        Task<List<Hardware>> GetAllAsync();
        Task<CustomGeneralResponses> MoveToUserAsync(List<Guid>? hardwaresID, Guid? userID, Guid? userWarehouseID);
        Task<CustomGeneralResponses> ReturnAsync(List<Guid> hardwaresID);
        Task<string> GenerateQR(List<Guid>? idList);
        Task<string> GenerateLabel(List<Guid>? idList);
        Task<List<Hardware>> GetByUserIdAsync(Guid userId);
        Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport);
        Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO);
        Task<CustomGeneralResponses> MarkAllHardware(List<Guid> hardwareId);
        Task<Hardware> GetByIdAsync(Guid id);
    }
}
