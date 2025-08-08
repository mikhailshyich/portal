using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class UserDepartmentDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string ShortTitle { get; set; } = string.Empty;
    }
}
