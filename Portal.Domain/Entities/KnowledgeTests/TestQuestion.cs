using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.KnowledgeTests
{
    public class TestQuestion
    {
        public Guid Id { get; set; }
        public Guid KnowledgeTestId { get; set; }
        [MaxLength(350)]
        public string QuestionText { get; set; } = string.Empty;

        public List<QuestionAnswer>? QuestionAnswers { get; set; }
    }
}
