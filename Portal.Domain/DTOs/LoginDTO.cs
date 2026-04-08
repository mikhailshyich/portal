using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле обязательно для заполнения."), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
