using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public class HardwareService : IHardware
    {
        private readonly IHardwareDomain hardwareDomain;

        public HardwareService(IHardwareDomain hardwareDomain)
        {
            this.hardwareDomain = hardwareDomain;
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            return await hardwareDomain.AddAsync(request);
        }

        public async Task<byte[]> Export()
        {
            return await hardwareDomain.Export();
        }

        public async Task<string> GenerateLabel(List<Guid>? idList)
        {
            return await hardwareDomain.GenerateLabel(idList);
        }

        public async Task<string> GenerateQR(List<Guid>? idList)
        {
            return await hardwareDomain.GenerateQR(idList);
        }

        public async Task<List<Hardware>> GetAllAsync()
        {
            return await hardwareDomain.GetAllAsync();
        }

        public async Task<Hardware> GetByIdAsync(Guid id)
        {
            return await hardwareDomain.GetByIdAsync(id);
        }

        public async Task<List<Hardware>> GetByUserIdAsync(Guid userId)
        {
            return await hardwareDomain.GetByUserIdAsync(userId);
        }

        public async Task<CustomGeneralResponses> Import(List<HardwareImportDTO> hardwareImport)
        {
            return await hardwareDomain.Import(hardwareImport);
        }

        public async Task<CustomGeneralResponses> MarkAllHardware(MarkAllHardwareDTO markAllHardwareDTO)
        {
            return await hardwareDomain.MarkAllHardware(markAllHardwareDTO);
        }

        public async Task<CustomGeneralResponses> MarkHardware(MarkHardwareDTO markHardwareDTO)
        {
            return await hardwareDomain.MarkHardware(markHardwareDTO);
        }

        public async Task<CustomGeneralResponses> MoveToUserAsync(HardwareMoveDTO moveDTO)
        {
            return await hardwareDomain.MoveToUserAsync(moveDTO);
        }

        public async Task<CustomGeneralResponses> ReturnAsync(HardwareReturnDTO returnDTO)
        {
            return await hardwareDomain.ReturnAsync(returnDTO);
        }

        public async Task<Hardware> UpdateAsync(HardwareUpdateDTO updateDTO)
        {
            return await hardwareDomain.UpdateAsync(updateDTO);
        }

        public async Task<CustomGeneralResponses> WriteOff(HardwareWriteOffDTO writeOffDTO)
        {
            return await hardwareDomain.WriteOff(writeOffDTO);
        }
    }
}
