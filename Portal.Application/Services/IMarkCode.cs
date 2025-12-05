using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;

namespace Portal.Application.Services
{
    public interface IMarkCode
    {
        Task<List<MarkCode>> AddAsync(MarkCodeDTO markCodeDTO);
    }
}
