namespace Portal.Domain.DTOs
{
    public class HardwareImportDTO
    {
        public Guid ResponsibleId { get; set; }
        public Guid MainWarehouseId { get; set; }
        public Guid CategoryHardwareId { get; set; }
        public Guid DocumentExternalSystemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Count { get; set; }
        public string InventoryNumberExternalSystem { get; set; } = string.Empty;
        public string TTN { get; set; } = string.Empty;
    }
}
