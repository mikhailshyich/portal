using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;

namespace Portal.Infrastructure.Data
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options) { 
            //Database.EnsureCreated();
            }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<MainWarehouse> MainWarehouses { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<CategoryHardware> CategoriesHardware { get; set; }
        public DbSet<DocumentExternalSystem> DocumentsExternalSystem { get; set; }
        public DbSet<UserHardware> UsersHardware { get; set; }
        public DbSet<UserWarehouse> UserWarehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //passwordHash admin %*Rfrnfrdsikj017(:
            //passwordHash user %*Rfrnfrdsikj@
            modelBuilder.Entity<User>().HasData(
                    new User { Id = Guid.Parse("F29533DD-9A3C-4889-D468-08DDD5A47B7A"), UserDepartmentId = Guid.Parse("D7FA6B79-CF7B-442E-32E6-08DDD5A32CAC"), Username = "admin", UserRoleId = Guid.Parse("F29533DD-9A3C-4899-D468-08DDD5A47B7A"), Email = "admin@admin.by", PasswordHash = "AQAAAAIAAYagAAAAEEnHKp66I1iY4a6WutPESx3dIQF0V/ITse74a7euQmBiSo8E516lhTSbFbEqJVAKQw==" },
                    new User { Id = Guid.Parse("D7FA6B79-CF3B-442E-37E6-08DDD5A32CAC"), UserDepartmentId = Guid.Parse("D7FA6B79-CF7B-442E-32E6-08DDD5A32CAC"), Username = "user", UserRoleId = Guid.Parse("D7FA6B79-CF7B-442E-37E6-08DDD5A32CAC"), Email = "user@user.by", PasswordHash = "AQAAAAIAAYagAAAAEOVSg/5PKFU0eFXRm9R6j5GvdEhsxlIymU+I51+5Y/+gQX+c7AHCeu/ZT5ByOLFk7w==" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole { Id = Guid.Parse("F29533DD-9A3C-4899-D468-08DDD5A47B7A"), Title = "admin", PublicTitle = "Администратор" },
                    new UserRole { Id = Guid.Parse("D7FA6B79-CF7B-442E-37E6-08DDD5A32CAC"), Title = "user", PublicTitle = "Пользователь" }
            );

            modelBuilder.Entity<UserDepartment>().HasData(
                    new UserDepartment { Id = Guid.Parse("D7FA6B79-CF7B-442E-32E6-08DDD5A32CAC"), Title = "Отдел по умолчанию", ShortTitle = "" }
            );

            modelBuilder.Entity<MainWarehouse>().HasData(
                new MainWarehouse { Id = Guid.Parse("c9006a11-ac35-4df9-0c93-08ddd89f84c8"), UserDepartmentId = Guid.Parse("D7FA6B79-CF7B-442E-32E6-08DDD5A32CAC"), Title = "Основной склад" }
                );

            modelBuilder.Entity<UserWarehouse>().HasData(
                new UserWarehouse { Id = Guid.Parse("0588a0e8-fdc8-4310-1850-08ddd8a42ead"), UserId = Guid.Parse("F29533DD-9A3C-4889-D468-08DDD5A47B7A"), Title = "Склад пользователя по умолчанию" },
                new UserWarehouse { Id = Guid.Parse("0588a0e8-fdc8-4317-1850-08ddd8a42ead"), UserId = Guid.Parse("D7FA6B79-CF3B-442E-37E6-08DDD5A32CAC"), Title = "Склад пользователя по умолчанию" }
                );
        }
    }
}
