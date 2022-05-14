namespace Website.Backend.Infrastructure.Interfaces
{
    public interface IEmailNotificationService
    {
        public Task SendEmailAsync(string toEmail, string subject, string htmlBody);
    }
}
