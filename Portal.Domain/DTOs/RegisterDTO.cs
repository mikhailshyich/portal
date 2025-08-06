using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
