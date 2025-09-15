using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class UserDepartmentDTO
    {
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [Display(Name = "Полное наименование")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(50)]
        public string ShortTitle { get; set; } = string.Empty;
    }
}
