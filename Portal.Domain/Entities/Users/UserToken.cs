namespace Portal.Domain.Entities.Users
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime DateTimeExpiredToken { get; set; }

        public List<User> Users { get; set; } = [];
        public User? User { get; set; }
    }
}
