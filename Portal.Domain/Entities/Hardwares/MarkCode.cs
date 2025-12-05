using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.Entities.Hardwares
{
    public class MarkCode
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? HardwareId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarkCodeNumber { get; set; }
        public bool Used { get; set; }

        public Hardware? Hardware { get; set; }
    }
}
