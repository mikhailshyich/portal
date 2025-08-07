using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class UserRoleDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string PublicTitle { get; set; } = string.Empty;
    }
}
