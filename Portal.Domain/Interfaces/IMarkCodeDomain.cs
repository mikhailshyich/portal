using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;

namespace Portal.Domain.Interfaces
{
    public interface IMarkCodeDomain
    {
        Task<List<MarkCode>> AddAsync(MarkCodeDTO markCodeDTO);
    }
}
