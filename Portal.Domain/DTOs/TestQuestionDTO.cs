using Portal.Domain.Entities.KnowledgeTests;

namespace Portal.Domain.DTOs
{
    public class TestQuestionDTO
    {
        public Guid KnowledgeTestId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
    }
}
