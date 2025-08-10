using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class HardwareRepository : IHardwareDomain
    {
        private readonly PortalDbContext context;

        public HardwareRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddAsync(HardwareDTO request)
        {
            if(request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");
            
            var hardwareCount = request.Count;
            var hardwareList = new List<Hardware>();

            for(var i=0; i < hardwareCount; i++)
            {
                var hardware = new Hardware()
                {
                    MainWarehouseId = request.MainWarehouseId,
                    CategoryHardwareId = request.CategoryHardwareId,
                    DocumentExternalSystemId = request.DocumentExternalSystemId,
                    Title = request.Title,
                    Description = request.Description,
                    Count = 1,
                    InventoryNumberExternalSystem = request.InventoryNumberExternalSystem,
                    TTN = request.TTN,
                    DateTimeAdd = DateTime.Now,
                    FileNameImage = request.FileNameImage,
                };
                hardwareList.Add(hardware);
            }

            context.AddRange(hardwareList);
            await context.SaveChangesAsync();

            return new CustomGeneralResponses(true, "Оборудование успешно добавлено.", hardwareList);
        }

        public async Task<List<Hardware>> GetAllAsync()
        {
            return await context.Hardwares.ToListAsync();
        }
    }
}
