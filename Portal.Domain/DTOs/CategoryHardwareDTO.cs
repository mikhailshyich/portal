using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class CategoryHardwareDTO
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        public string ShortTitle { get; set; } = string.Empty;
    }
}
