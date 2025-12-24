using Portal.Domain.Entities.History;
using Portal.Domain.Interfaces;

namespace Portal.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryDomain historyDomain;

        public HistoryService(IHistoryDomain historyDomain)
        {
            this.historyDomain = historyDomain;
        }

        public async Task<List<History>> GetByHardwareIdAsync(Guid hardwareId)
        {
            return await historyDomain.GetByHardwareIdAsync(hardwareId);
        }
    }
}
