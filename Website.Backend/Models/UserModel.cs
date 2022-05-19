namespace Website.Backend.Models
{
    public class UserModel
    {
        public long Id { get; set; }

        public string EmailAddress { get; set; } = String.Empty;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
