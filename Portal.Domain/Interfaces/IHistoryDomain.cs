using Portal.Domain.Entities.History;

namespace Portal.Domain.Interfaces
{
    public interface IHistoryDomain
    {
        Task<List<History>> GetByHardwareIdAsync(Guid hardwareId);
    }
}
