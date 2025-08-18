using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.DTOs
{
    public class HardwareDTO
    {
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public Guid MainWarehouseId { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public Guid CategoryHardwareId { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public Guid DocumentExternalSystemId { get; set; }
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(1, 99999, ErrorMessage = "Количество должно быть больше 0.")]
        public int Count { get; set; }
        public string InventoryNumberExternalSystem { get; set; } = string.Empty;
        public string TTN { get; set; } = string.Empty;
        public DateTime DateTimeAdd { get; set; }
        public string FileNameImage { get; set; } = string.Empty;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryNumber { get; set; }
    }
}
