using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Responses;

namespace Portal.WEB.Services
{
    public class DocumentExternalSystemServiceWEB : IDocumentExternalSystemServiceWEB
    {
        private readonly HttpClient httpClient;
        private readonly string BaseURI = "/api/DocumentExternalSystem";
        public DocumentExternalSystemServiceWEB(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomGeneralResponses> AddAsync(DocumentExternalSystemDTO request)
        {
            var document = await httpClient.PostAsJsonAsync($"{BaseURI}/add", request);
            var response = await document.Content.ReadFromJsonAsync<CustomGeneralResponses>();
            return response!;
        }

        public Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DocumentExternalSystem>> GetAllAsync()
        {

            var document = await httpClient.GetAsync($"{BaseURI}/all");
            var response = await document.Content.ReadFromJsonAsync<List<DocumentExternalSystem>>();
            return response!;
        }

        public Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomGeneralResponses> UpdateAsync(DocumentExternalSystem request)
        {
            throw new NotImplementedException();
        }
    }
}
