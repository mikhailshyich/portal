using Portal.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Entities.History
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [MaxLength(40)]
        public string OperationType { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; } = string.Empty;
        public DateTime DateTimeChanges { get; set; }

        public User? User { get; set; }

        public History(Guid userId, string operationType, string? description, DateTime dateTimeChanges)
        {
            this.UserId = userId;
            this.OperationType = operationType;
            this.Description = description;
            this.DateTimeChanges = dateTimeChanges;
        }
    }
}
