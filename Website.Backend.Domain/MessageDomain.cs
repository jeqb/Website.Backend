namespace Website.Backend.Domain
{
    public class MessageDomain
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}