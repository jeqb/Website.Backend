namespace Website.Backend.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }
    }
}
