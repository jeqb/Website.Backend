namespace Website.Backend.Infrastructure.Email
{
    public interface IEmailNotificationService
    {
        public Task SendEmailAsync(string toEmail, string subject, string htmlBody);
    }
}
