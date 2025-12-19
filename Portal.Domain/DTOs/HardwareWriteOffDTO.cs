
namespace Portal.Domain.DTOs
{
    public class HardwareWriteOffDTO
    {
        public Guid ResponsibleId { get; set; }
        public List<Guid> HardwareIdList { get; set; }
    }
}
