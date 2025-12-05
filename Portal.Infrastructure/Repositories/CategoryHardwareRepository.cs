using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.History;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class CategoryHardwareRepository : ICategoryHardwareDomain
    {
        private readonly PortalDbContext context;

        public CategoryHardwareRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddAsync(CategoryHardwareDTO request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var categoryTitle = await context.CategoriesHardware.AnyAsync(r => r.Title == request.Title);
            if (categoryTitle) return new CustomGeneralResponses(false, $"Категория [{request.Title}] уже существует.");

            var categoryShortTitle = await context.CategoriesHardware.AnyAsync(c=> c.ShortTitle == request.ShortTitle);
            if (categoryShortTitle) return new CustomGeneralResponses(false, $"Выберите другое сокращение. [{request.ShortTitle}] уже используется.");

            var category = new CategoryHardware()
            {
                Title = request.Title,
                ShortTitle = request.ShortTitle,
            };

            context.CategoriesHardware.Add(category);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Категория успешно создана.", category);
        }

        public async Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return new CustomGeneralResponses(false, "Guid не может быть пустым.");

            var category = await context.CategoriesHardware.FindAsync(id);
            if (category is null) return new CustomGeneralResponses(false, "Категория не найдена.");

            context.Remove(category);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Категория {category.Title} успешно удалёна.");
        }

        public async Task<List<CategoryHardware>> GetAllAsync()
        {
            return await context.CategoriesHardware.ToListAsync();
        }

        public async Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return new CustomGeneralResponses(false, "Guid не может быть пустым.");

            var category = await context.CategoriesHardware.FindAsync(id);
            if (category is null) return new CustomGeneralResponses(false, "Категория не найдена.");

            return new CustomGeneralResponses(true, "Категория успешно найдена.", category);
        }

        public async Task<CustomGeneralResponses> UpdateAsync(CategoryHardware request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var category = await context.CategoriesHardware.FindAsync(request.Id);
            if (category is null) return new CustomGeneralResponses(false, "Категория не найдена.");

            category.Title = request.Title;
            category.ShortTitle = request.ShortTitle;
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Категория успешно обновлёна.", category);
        }
    }
}
