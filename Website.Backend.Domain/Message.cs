using Azure;

namespace Website.Backend.Domain
{
    public class Message
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public bool IsRead { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }
}
