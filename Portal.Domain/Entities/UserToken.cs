namespace Portal.Domain.Entities
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string JWTToken { get; set; } = string.Empty;
        public DateTime DateTimeExpiredJWT { get; set; }
        public string RefToken { get; set; } = string.Empty;
        public DateTime DateTimeExpiredRef { get; set; }
    }
}
