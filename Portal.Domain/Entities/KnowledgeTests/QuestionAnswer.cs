using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.KnowledgeTests
{
    public class QuestionAnswer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        [MaxLength(150)]
        public string AnswerText { get; set; } = string.Empty;
        public bool IsRight { get; set; }
    }
}
