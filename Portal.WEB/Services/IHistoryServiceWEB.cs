using Portal.Domain.Entities.History;

namespace Portal.WEB.Services
{
    public interface IHistoryServiceWEB
    {
        Task<List<History>> GetByHardwareIdAsync(Guid hardwareId);
    }
}
