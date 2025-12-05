using Portal.Domain.Entities.Users;
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
        public Guid? UserId { get; set; }
        public Guid? UserWarehouseId {  get; set; }
        public Guid? MarkCode { get; set; }
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(150)]
        public string NameForLabel { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Description { get; set; } = string.Empty;
        public int Count { get; set; }
        [MaxLength(20)]
        public string InventoryNumberExternalSystem { get; set; } = string.Empty;
        [MaxLength(60)]
        public string TTN { get; set; } = string.Empty;
        public DateTime DateTimeAdd { get; set; }
        public string FileNameImage { get; set; } = string.Empty;
        public string CombinedInvNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public MainWarehouse? MainWarehouse { get; set; }
        public CategoryHardware? CategoryHardware { get; set; }
        public DocumentExternalSystem? DocumentExternalSystem { get; set; }
        public User? User { get; set; }
        public UserWarehouse? UserWarehouse { get; set; }
    }
}
