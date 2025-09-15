using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class UserWarehouseDTO
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;
    }
}
