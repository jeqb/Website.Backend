namespace Website.Backend.Domain
{
    public class Message
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}