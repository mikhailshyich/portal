using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class MainWarehouseDTO
    {
        [Required]
        public Guid UserDepartmentId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
