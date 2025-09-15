using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class MainWarehouseDTO
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        public Guid UserDepartmentId { get; set; }
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
    }
}
