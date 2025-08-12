using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class MainWarehouseDTO
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        public Guid UserDepartmentId { get; set; }
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
