namespace Website.Backend.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; } = String.Empty;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;

        public DateTimeOffset CreatedDateTime { get; set; }

        public DateTimeOffset UpdatedDateTime { get; set; }
    }
}