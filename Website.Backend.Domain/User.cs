using Azure;

namespace Website.Backend.Domain
{
    public class User
    {
        public long Id { get; set; }

        public string EmailAddress { get; set; } = String.Empty;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }
}
