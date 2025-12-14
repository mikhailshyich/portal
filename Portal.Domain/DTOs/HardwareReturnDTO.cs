namespace Portal.Domain.DTOs
{
    public class HardwareReturnDTO
    {
        public List<Guid> HardwareIdList { get; set; }
        public Guid ResponsibleId { get; set; }
    }
}
