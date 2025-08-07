using Portal.Domain.Entities.Warehouses;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Users
{
    public class UserDepartment
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortTitle { get; set; } = string.Empty;

        public List<User> Users { get; set; } = [];
        public List<MainWarehouse> MainWarehouses { get; set; } = [];
    }
}
