using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class DocumentExternalSystemRepository : IDocumentExternalSystemDomain
    {
        private readonly PortalDbContext context;

        public DocumentExternalSystemRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddAsync(DocumentExternalSystemDTO request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var result = await context.DocumentsExternalSystem.AnyAsync(r => r.Title == request.Title);
            if (result) return new CustomGeneralResponses(false, "Такое наименование документа уже существует.");

            var document = new DocumentExternalSystem()
            {
                Title = request.Title,
                ShortTitle = request.ShortTitle,
            };

            context.DocumentsExternalSystem.Add(document);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Документ успешно создан.", document);
        }

        public async Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return new CustomGeneralResponses(false, "Guid не может быть пустым.");

            var document = await context.DocumentsExternalSystem.FindAsync(id);
            if (document is null) return new CustomGeneralResponses(false, "Наименование документа не найдено.");

            context.Remove(document);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Документ {document.Title} успешно удалён.");
        }

        public async Task<List<DocumentExternalSystem>> GetAllAsync()
        {
            return await context.DocumentsExternalSystem.ToListAsync();
        }

        public async Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return new CustomGeneralResponses(false, "Guid не может быть пустым.");

            var document = await context.DocumentsExternalSystem.FindAsync(id);
            if (document is null) return new CustomGeneralResponses(false, "Документ не найден.");

            return new CustomGeneralResponses(true, "Документ успешно найден.", document);
        }

        public async Task<CustomGeneralResponses> UpdateAsync(DocumentExternalSystem request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var document = await context.DocumentsExternalSystem.FindAsync(request.Id);
            if (document is null) return new CustomGeneralResponses(false, "Документ не найден.");

            document.Title = request.Title;
            document.ShortTitle = request.ShortTitle;
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Документ успешно обновлён.", document);
        }
    }
}
