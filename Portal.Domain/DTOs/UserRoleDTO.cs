using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class UserRoleDTO
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string PublicTitle { get; set; } = string.Empty;
    }
}
