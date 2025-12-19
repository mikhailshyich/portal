using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public interface IHardwareServiceWEB
    {
        Task<CustomGeneralResponses> AddAsync(HardwareDTO request);
        Task<List<Hardware>> GetAllAsync();
        Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO);
        Task<string> GenerateQR(List<Guid>? idList);
        Task<string> GenerateLabel(List<Guid>? idList);
        Task<List<Hardware>> GetByUserIdAsync(Guid userId);
        Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport);
        Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO);
        Task<CustomGeneralResponses> MarkAllHardware(List<Guid> hardwareId);
        Task<Hardware> GetByIdAsync(Guid id);
        Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO);
        Task<Hardware> UpdateAsync(HardwareUpdateDTO updateDTO);
        Task<CustomGeneralResponses> WriteOff(HardwareWriteOffDTO writeOffDTO);
    }
}
