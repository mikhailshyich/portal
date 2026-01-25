using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.DTOs
{
    public class QuestionAnswerDTO
    {
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; } = string.Empty;
        public bool IsRight { get; set; }
    }
}
