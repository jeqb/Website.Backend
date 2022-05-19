namespace Website.Backend.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public DateTime? Created { get; set; }
    }
}
