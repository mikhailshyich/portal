using Portal.Domain.Entities.Warehouses;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserRoleId { get; set; }
        public Guid UserDepartmentId { get; set; }
        [MaxLength(40)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(40)]
        public string Patronymic { get; set; } = string.Empty; //отчество
        [MaxLength(20)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Specialization { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public UserRole? UserRole { get; set; }
        public UserDepartment? UserDepartment { get; set; }
        //public List<Hardware> Hardwares { get; set; } = [];
        public List<UserWarehouse> UserWarehouses { get; set; } = [];
    }
}
