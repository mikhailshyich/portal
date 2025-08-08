using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;
using Portal.Domain.Interfaces;
using Portal.Domain.Responses;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class UserDepartmentRepository : IUserDepartmentDomain
    {
        private readonly PortalDbContext context;

        public UserDepartmentRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<CustomGeneralResponses> AddAsync(UserDepartmentDTO request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var department = await context.UserDepartments.AnyAsync(r => r.Title == request.Title);
            if (department) return new CustomGeneralResponses(false, "Такой отдел уже существует.");

            var userDepartment = new UserDepartment()
            {
                Title = request.Title,
                ShortTitle = request.ShortTitle,
            };

            context.UserDepartments.Add(userDepartment);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Отдел успешно создан.", userDepartment);
        }

        public async Task<CustomGeneralResponses> DeleteAsync(Guid id)
        {
            if(id == Guid.Empty) return new CustomGeneralResponses(false, "Guid не может быть пустым.");

            var department = await context.UserDepartments.FindAsync(id);
            if (department is null) return new CustomGeneralResponses(false, "Отдел не найден.");

            context.Remove(department);
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, $"Отдел {department.Title} успешно удалён.");
        }

        public async Task<List<UserDepartment>> GetAllAsync()
        {
            return await context.UserDepartments.ToListAsync();
        }

        public async Task<CustomGeneralResponses> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return new CustomGeneralResponses(false, "Guid не может быть пустым.");

            var department = await context.UserDepartments.FindAsync(id);
            if (department is null) return new CustomGeneralResponses(false, "Отдел не найден.");

            return new CustomGeneralResponses(true, "Департамент успешно найден.", department);
        }

        public async Task<CustomGeneralResponses> UpdateAsync(UserDepartment request)
        {
            if (request is null) return new CustomGeneralResponses(false, "Передаваемый объект равен null.");

            var department = await context.UserDepartments.FindAsync(request.Id);
            if (department is null) return new CustomGeneralResponses(false, "Департамент не найден.");

            department.Title = request.Title;
            department.ShortTitle = request.ShortTitle;
            await context.SaveChangesAsync();
            return new CustomGeneralResponses(true, "Департамент успешно обновлён.", department);
        }
    }
}
