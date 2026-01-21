using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IHardwareDomain
    {
        Task<CustomGeneralResponses> AddAsync(HardwareDTO request);
        Task<List<Hardware>> GetAllAsync();
        Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO);
        Task<CustomGeneralResponses> GiveToUserAsync(HardwareMoveDTO giveDTO);
        Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO);
        Task<CustomGeneralResponses> RepairAsync(HardwareRepairDTO repairDTO);
        Task<CustomGeneralResponses> ReturnRepairAsync(HardwareRepairDTO repairDTO);
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
