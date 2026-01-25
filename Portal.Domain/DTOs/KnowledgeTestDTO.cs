namespace Portal.Domain.DTOs
{
    public class KnowledgeTestDTO
    {
        public Guid ResponsibleId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string TestName { get; set; } = string.Empty;
    }
}
