using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public interface IDocumentExternalSystemServiceWEB
    {
        Task<CustomGeneralResponses> AddAsync(DocumentExternalSystemDTO request);
        Task<CustomGeneralResponses> UpdateAsync(DocumentExternalSystem request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<DocumentExternalSystem>> GetAllAsync();
        Task<CustomGeneralResponses> GetByIdAsync(Guid id);
    }
}
