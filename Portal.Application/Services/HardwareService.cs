using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
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

        public async Task<string> GenerateQR(Guid? id, List<Guid>? idList)
        {
            return await hardwareDomain.GenerateQR(id, idList);
        }

        public async Task<List<Hardware>> GetAllAsync()
        {
            return await hardwareDomain.GetAllAsync();
        }
    }
}
