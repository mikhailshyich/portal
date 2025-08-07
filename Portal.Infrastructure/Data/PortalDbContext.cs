using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using Portal.Domain.Entities.Warehouses;

namespace Portal.Infrastructure.Data
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<MainWarehouse> MainWarehouses { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<CategoryHardware> CategoriesHardware { get; set; }
        public DbSet<DocumentExternalSystem> DocumentsExternalSystem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole { Id = Guid.Parse("F29533DD-9A3C-4899-D468-08DDD5A47B7A"), Title = "admin", PublicTitle = "Администратор" },
                    new UserRole { Id = Guid.Parse("D7FA6B79-CF7B-442E-37E6-08DDD5A32CAC"), Title = "user", PublicTitle = "Пользователь" }
            );

            //passwordHash admin %*Rfrnfrdsikj017(:
            //passwordHash user %*Rfrnfrdsikj@
            modelBuilder.Entity<User>().HasData(
                    new User { Id = Guid.Parse("F29533DD-9A3C-4889-D468-08DDD5A47B7A"), Username = "admin", UserRoleId = Guid.Parse("F29533DD-9A3C-4899-D468-08DDD5A47B7A"), Email = "admin@admin.by", PasswordHash = "AQAAAAIAAYagAAAAEEnHKp66I1iY4a6WutPESx3dIQF0V/ITse74a7euQmBiSo8E516lhTSbFbEqJVAKQw==" },
                    new User { Id = Guid.Parse("D7FA6B79-CF3B-442E-37E6-08DDD5A32CAC"), Username = "user", UserRoleId = Guid.Parse("D7FA6B79-CF7B-442E-37E6-08DDD5A32CAC"), Email = "user@user.by", PasswordHash = "AQAAAAIAAYagAAAAEOVSg/5PKFU0eFXRm9R6j5GvdEhsxlIymU+I51+5Y/+gQX+c7AHCeu/ZT5ByOLFk7w==" }
            );
        }
    }
}
