namespace Portal.Domain.DTOs
{
    public class MarkAllHardwareDTO
    {
        public Guid ResponsibleId { get; set; }
        public List<Guid> HardwareIdList { get; set; }
    }
}
