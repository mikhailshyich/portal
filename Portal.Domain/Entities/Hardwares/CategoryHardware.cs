using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Hardwares
{
    public class CategoryHardware
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(10)]
        public string ShortTitle { get; set; } = string.Empty;

        public List<Hardware> Hardwares { get; set; } = [];
    }
}
