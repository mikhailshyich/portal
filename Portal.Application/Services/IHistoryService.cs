using Portal.Domain.Entities.History;

namespace Portal.Application.Services
{
    public interface IHistoryService
    {
        Task<List<History>> GetByHardwareIdAsync(Guid hardwareId);
    }
}
