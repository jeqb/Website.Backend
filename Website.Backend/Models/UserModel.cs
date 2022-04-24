namespace Website.Backend.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; } = String.Empty;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }
    }
}
