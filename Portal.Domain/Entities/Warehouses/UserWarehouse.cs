using Portal.Domain.Entities.Hardwares;
using Portal.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Warehouses
{
    public class UserWarehouse
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;

        public User? User { get; set; }
        public List<Hardware> Hardwares { get; set; } = [];
    }
}
