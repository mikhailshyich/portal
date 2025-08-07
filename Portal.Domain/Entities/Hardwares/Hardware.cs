using Portal.Domain.Entities.Warehouses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.Entities.Hardwares
{
    public class Hardware
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MainWarehouseId { get; set; }
        public Guid CategoryHardwareId { get; set; }
        public Guid DocumentExternalSystemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string InventoryNumberExternalSystem { get; set; } = string.Empty;
        public string TTN { get; set; } = string.Empty;
        public DateTime DateTimeAdd { get; set; }
        public string FileNameImage { get; set; } = string.Empty;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryNumber { get; set; }

        public MainWarehouse? MainWarehouse { get; set; }
        public CategoryHardware? CategoryHardware { get; set; }
        public DocumentExternalSystem? DocumentExternalSystem { get; set; }
    }
}
