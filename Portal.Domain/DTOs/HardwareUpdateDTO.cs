namespace Portal.Domain.DTOs
{
    public class HardwareUpdateDTO
    {
        public Guid HardwareId { get; set; }
        public Guid? ResponsibleId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UserWarehouseId { get; set; }
        public string? NameForLabel { get; set; } = string.Empty;
        public string? InventoryNumberExternalSystem { get; set; } = string.Empty;
    }
}
