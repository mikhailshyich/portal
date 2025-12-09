using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Users
{
    public class UserEdit
    {
        public Guid Id { get; set; }
        public Guid UserRoleId { get; set; }
        public Guid UserDepartmentId { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [MaxLength(40)]
        public string? FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [MaxLength(40)]
        public string? LastName { get; set; } = string.Empty;
        [MaxLength(40)]
        public string? Patronymic { get; set; } = string.Empty; //отчество
        public string? Specialization { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        [MaxLength(20)]
        public string Username { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Проверьте правильность введённого email.")]
        [MaxLength(50)]
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Введённые пароли не совпадают.")]
        public string? ConfirmPassword { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public UserEdit() { }

        public UserEdit(Guid id, Guid userRoleId, Guid userDepartmentId, string? firstName, string? lastname, string? patronymic,
                        string? specialization, string? email, bool isActive)
        {
            this.Id = id;
            this.UserRoleId = userRoleId;
            this.UserDepartmentId = userDepartmentId;
            this.FirstName = firstName;
            this.LastName = lastname;
            this.Patronymic = patronymic;
            this.Specialization = specialization;
            this.Email = email;
            this.IsActive = isActive;
        }
    }
}
