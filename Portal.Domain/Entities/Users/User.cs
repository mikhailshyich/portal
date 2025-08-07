using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserRoleId { get; set; }
        public Guid UserDepartmentId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public UserRole? UserRole {  get; set; }
        public UserDepartment? UserDepartment { get; set; }
    }
}
