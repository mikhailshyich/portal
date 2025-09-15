using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class RegisterDTO
    {
        public Guid UserRoleId { get; set; }
        public Guid UserDepartmentId { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [MaxLength(40)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(40)]
        public string Patronymic { get; set; } = string.Empty; //отчество
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [MaxLength(20)]
        public string Username { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Проверьте правильность введённого email.")]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Введённые пароли не совпадают.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
