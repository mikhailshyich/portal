using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class UserDepartmentDTO
    {
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [Display(Name = "Полное наименование")]
        public string Title { get; set; } = string.Empty;
        public string ShortTitle { get; set; } = string.Empty;
    }
}
