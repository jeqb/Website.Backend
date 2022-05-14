using Azure.Storage.Queues;
using System.Text.Json;

namespace Website.Backend.Infrastructure.Email
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly QueueClient _queueClient;

        public EmailNotificationService(string queueConnectionString, string queueName)
        {
            // sort of violates IOC, but want to keep API ignorant of QueueClient.
            _queueClient = new QueueClient(queueConnectionString, queueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            EmailRequestModel queueMessage = new()
            {
                ToEmail = toEmail,
                Subject = subject,
                HtmlBody = htmlBody
            };

            string serializedMessage = JsonSerializer.Serialize(queueMessage,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            await _queueClient.SendMessageAsync(serializedMessage);
        }
    }
}
