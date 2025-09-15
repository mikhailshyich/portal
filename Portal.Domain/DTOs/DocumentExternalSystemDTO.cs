using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class DocumentExternalSystemDTO
    {
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(10)]
        public string ShortTitle { get; set; } = string.Empty;
    }
}
