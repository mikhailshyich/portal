namespace Portal.Domain.DTOs
{
    public class RefreshTokenDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime DateTimeExpired { get; set; }
    }
}
