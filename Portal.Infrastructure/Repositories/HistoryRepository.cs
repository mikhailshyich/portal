using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities.History;
using Portal.Domain.Entities.Users;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class HistoryRepository : IHistoryDomain
    {
        private readonly PortalDbContext context;

        public HistoryRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<List<History>> GetByHardwareIdAsync(Guid hardwareId)
        {
            if (hardwareId == Guid.Empty) return null!;

            var historyList = await context.HistoryEntries.Where(h=> h.HardwareId == hardwareId).OrderByDescending(h=>h.DateTimeChanges).ToListAsync();

            if(historyList.Count == 0) return null!;

            foreach(var history in historyList)
            {
                if (history.ResponsibleId != null)
                {
                    var responsible = await context.Users.FirstOrDefaultAsync(u=>u.Id == history.ResponsibleId);
                    UserView userView = new(responsible.Id, responsible.UserRoleId, responsible.UserDepartmentId, responsible.FirstName, responsible.LastName,
                        responsible.Patronymic, responsible.Specialization, responsible.Email, responsible.IsActive);
                    if (responsible != null)
                    {
                        history.ResponsibleUser = userView;
                        history.ResponsibleString = $"{responsible.LastName} {responsible.FirstName}";
                    }
                }

                if (history.SenderId != null)
                {
                    var sender = await context.Users.FirstOrDefaultAsync(u => u.Id == history.SenderId);
                    UserView userView = new(sender.Id, sender.UserRoleId, sender.UserDepartmentId, sender.FirstName, sender.LastName,
                        sender.Patronymic, sender.Specialization, sender.Email, sender.IsActive);
                    if (sender != null)
                    {
                        history.SenderUser = userView;
                        history.SenderString = $"{sender.LastName} {sender.FirstName}";
                    }
                }

                if (history.RecipientId != null)
                {
                    var recipient = await context.Users.FirstOrDefaultAsync(u => u.Id == history.RecipientId);
                    UserView userView = new(recipient.Id, recipient.UserRoleId, recipient.UserDepartmentId, recipient.FirstName, recipient.LastName,
                        recipient.Patronymic, recipient.Specialization, recipient.Email, recipient.IsActive);
                    if (recipient != null)
                    {
                        history.RecipienUser = userView;
                        history.RecipientString = $"{recipient.LastName} {recipient.FirstName}";
                    }
                        
                }

                if (history.WarehouseId != null)
                {
                    var warehouse = await context.MainWarehouses.FirstOrDefaultAsync(u => u.Id == history.WarehouseId);
                    
                    if (warehouse != null)
                    {
                        history.MainWarehouse = warehouse;
                        history.MainWarehouseString = warehouse.Title;
                    }
                }

                if (history.UserWarehouseId != null)
                {
                    var userWarehouse = await context.UserWarehouses.FirstOrDefaultAsync(u => u.Id == history.UserWarehouseId);

                    if (userWarehouse != null)
                    {
                        history.UserWarehouse = userWarehouse;
                        history.UserWarehouseString = userWarehouse.Title;
                    }
                }
            }

            return historyList;
        }
    }
}
