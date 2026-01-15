using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public Guid ResponsibleId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UserWarehouseId { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения.")]
        public string Title { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(1, 99999, ErrorMessage = "Количество должно быть больше 0.")]
        public int Count { get; set; }
        [MaxLength(20)]
        public string InventoryNumberExternalSystem { get; set; } = string.Empty;
        [MaxLength(60)]
        public string TTN { get; set; } = string.Empty;
        public DateTime DateTimeAdd { get; set; }
        public string FileNameImage { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
    }
}
