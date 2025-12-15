namespace Portal.Domain.DTOs
{
    public class HardwareUpdateDTO
    {
        public Guid HardwareId { get; set; }
        public string? NameForLabel { get; set; } = string.Empty;
        public string? InventoryNumberExternalSystem { get; set; } = string.Empty;
    }
}
