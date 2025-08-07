using Portal.Domain.Entities.Hardwares;
using System.ComponentModel.DataAnnotations;
namespace Portal.Domain.Entities.Warehouses
{
    public class MainWarehouse
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Hardware> Hardwares { get; set; } = [];
    }
}
