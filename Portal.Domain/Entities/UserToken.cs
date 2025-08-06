namespace Portal.Domain.Entities
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime DateTimeExpiredToken { get; set; }
    }
}
