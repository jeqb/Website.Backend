namespace Website.Backend.Domain
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public bool IsRead { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public DateTimeOffset UpdatedDateTime { get; set; }
    }
}