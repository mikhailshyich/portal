using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.KnowledgeTests
{
    public class KnowledgeTest
    {
        public Guid Id { get; set; }
        public Guid ResponsibleId { get; set; }
        public Guid? DepartmentId { get; set; }
        [MaxLength(250)]
        public string TestName { get; set; } = string.Empty;
        public DateTime DateTimeCreated { get; set; }

        public List<TestQuestion>? TestQuestions { get; set; }
    }
}
