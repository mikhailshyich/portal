using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Hardwares
{
    public class DocumentExternalSystem
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortTitle { get; set; } = string.Empty;

        public List<Hardware> Hardwares { get; set; } = [];
    }
}
