using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Users
{
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PublicTitle { get; set; } = string.Empty;

        public List<User> Users { get; set; } = [];
    }
}
