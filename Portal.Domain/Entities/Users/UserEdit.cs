using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Users
{
    public class UserEdit
    {
        public Guid Id { get; set; }
        public Guid UserRoleId { get; set; }
        public Guid UserDepartmentId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Patronymic { get; set; } = string.Empty; //отчество
        public string? Specialization { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Введённые пароли не совпадают.")]
        public string? ConfirmPassword { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
