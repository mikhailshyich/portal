using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Domain.Interfaces
{
    public interface IDocumentExternalSystemDomain
    {
        Task<CustomGeneralResponses> AddAsync(DocumentExternalSystemDTO request);
        Task<CustomGeneralResponses> UpdateAsync(DocumentExternalSystem request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<DocumentExternalSystem>> GetAllAsync();
        Task<DocumentExternalSystem> GetByIdAsync(Guid id);
    }
}
