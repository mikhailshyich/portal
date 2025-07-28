using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Data
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
    }
}
