using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class HardwareUpdateDTO
    {
        public Guid HardwareId { get; set; }
        public Guid? ResponsibleId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UserWarehouseId { get; set; }
        public Guid? MainWarehouseId { get; set; }
        public Guid? CategoryHardwareId { get; set; }
        public Guid? DocumentExternalSystemId { get; set; }
        public string? Title { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Описание не больше 100 символов.")]
        public string? Description { get; set; } = string.Empty;
        public string? NameForLabel { get; set; } = string.Empty;
        [MaxLength(20, ErrorMessage = "Номер внешней системы не больше 20 символов.")]
        public string? InventoryNumberExternalSystem { get; set; } = string.Empty;
        [MaxLength(60, ErrorMessage = "Документ не больше 60 символов.")]
        public string? TTN { get; set; } = string.Empty;
        public string? SerialNumber { get; set; } = string.Empty;
    }
}
