using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities.History;
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

            return historyList;
        }
    }
}
