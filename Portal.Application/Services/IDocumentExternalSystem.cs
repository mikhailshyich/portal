using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public interface IDocumentExternalSystem
    {
        Task<CustomGeneralResponses> AddAsync(DocumentExternalSystemDTO request);
        Task<CustomGeneralResponses> UpdateAsync(DocumentExternalSystem request);
        Task<CustomGeneralResponses> DeleteAsync(Guid id);
        Task<List<DocumentExternalSystem>> GetAllAsync();
        Task<CustomGeneralResponses> GetByIdAsync(Guid id);
    }
}
