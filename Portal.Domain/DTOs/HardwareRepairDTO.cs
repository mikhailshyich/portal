namespace Portal.Domain.DTOs
{
    public class HardwareRepairDTO
    {
        public Guid ResponsibleId { get; set; }
        public List<Guid> HardwareIdList { get; set; }
        public string? Annotation { get; set; }
    }
}
