using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.Hardwares
{
    public class UserHardware
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid HardwareId { get; set; }
    }
}
