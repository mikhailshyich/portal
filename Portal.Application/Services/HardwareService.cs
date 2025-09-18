using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;
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

        public async Task<List<Hardware>> GetByUserIdAsync(Guid userId)
        {
            return await hardwareDomain.GetByUserIdAsync(userId);
        }

        public async Task<CustomGeneralResponses> MoveToUserAsync(List<Guid> hardwaresID, Guid userID, Guid userWarehouseID)
        {
            return await hardwareDomain.MoveToUserAsync(hardwaresID, userID, userWarehouseID);
        }
    }
}
