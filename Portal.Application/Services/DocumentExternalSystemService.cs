using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;

namespace Portal.Application.Services
{
    public class DocumentExternalSystemService : IDocumentExternalSystem
    {
        private readonly IDocumentExternalSystemDomain documentInterface;

        public DocumentExternalSystemService(IDocumentExternalSystemDomain documentInterface)
        {
            this.documentInterface = documentInterface;
        }

        public async Task<CustomGeneralResponses> AddAsync(DocumentExternalSystemDTO request)
        {
            return await documentInterface.AddAsync(request);
        }

        public async Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            return await documentInterface.DeleteAsync(id);
        }

        public async Task<List<DocumentExternalSystem>> GetAllAsync()
        {
            return await documentInterface.GetAllAsync();
        }

        public async Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            return await documentInterface.GetByIdAsync(id);
        }

        public async Task<CustomGeneralResponses> UpdateAsync(DocumentExternalSystem request)
        {
            return await documentInterface.UpdateAsync(request);
        }
    }
}
