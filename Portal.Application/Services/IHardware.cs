using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IHardware
    {
        Task<CustomGeneralResponses> AddAsync(HardwareDTO request);
        Task<List<Hardware>> GetAllAsync();
        Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO);
        Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO);
        Task<string> GenerateQR(List<Guid>? idList);
        Task<string> GenerateLabel(List<Guid>? idList);
        Task<List<Hardware>> GetByUserIdAsync(Guid userId);
        Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport);
        Task<Byte[]> Export();
        Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO);
        Task<CustomGeneralResponses> MarkAllHardware(MarkAllHardwareDTO markAllHardwareDTO);
        Task<Hardware> GetByIdAsync(Guid id);
        Task<Hardware> UpdateAsync(HardwareUpdateDTO updateDTO);
        Task<CustomGeneralResponses> WriteOff(HardwareWriteOffDTO writeOffDTO);
    }
}
