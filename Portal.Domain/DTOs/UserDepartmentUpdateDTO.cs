using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class UserDepartmentUpdateDTO
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ShortTitle { get; set; } = string.Empty;
    }
}
