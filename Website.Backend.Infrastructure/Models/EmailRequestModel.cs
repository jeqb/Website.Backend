namespace Website.Backend.Infrastructure.Models
{
    public class EmailRequestModel
    {
        public string ToEmail { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string HtmlBody { get; set; } = String.Empty;
    }
}
