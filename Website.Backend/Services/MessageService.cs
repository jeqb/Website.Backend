using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Extensions;
using Website.Backend.Infrastructure.Interfaces;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _logger;

        private readonly IRepository<Message> _messageRepository;

        private readonly IEmailNotificationService _emailNotificationService;

        private readonly string _ownerEmail;

        public MessageService(ILogger<MessageService> logger, IRepositoryFactory repositoryFactory,
            IEmailNotificationService emailNotificationService, string ownerEmail)
        {
            _logger = logger;

            _messageRepository = repositoryFactory.CreateMessageRepository();

            _emailNotificationService = emailNotificationService;

            _ownerEmail = ownerEmail;
        }

        public async Task<MessageModel> Create(MessageModel entity)
        {
            Message createdModel = await _messageRepository.Create(entity.ToDomain());

            string toEmail = entity.Email;
            string subject = "Message Received";
            // TODO: make this an html file or something. not just a lame string
            string htmlBody = "Thank you for reaching out. The owner will reach out to you shortly.";

            try
            {
                // email person who sent the message
                await _emailNotificationService.SendEmailAsync(toEmail, subject, htmlBody);

                // TODO: make an html template for this too or something.
                // mention who it's from.
                await _emailNotificationService.SendEmailAsync(_ownerEmail, "New Website Inquiry",
                    "New message received");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send email with Exception: {message}", ex.Message);
            }

            return createdModel.ToModel();
        }

        public async Task Delete(Guid id)
        {
            Message message = await _messageRepository.GetById(id);

            // no message was found
            if (message.Id != id)
            {
                return;
            }
            else
            {
                await _messageRepository.Delete(message);
            }
        }

        public async Task<IEnumerable<MessageModel>> GetAll()
        {
            IEnumerable<Message> results = await _messageRepository.GetAll();

            return results.Select(
                (message) => message.ToModel()
                );
        }

        public async Task<MessageModel> GetById(Guid id)
        {
            Message message = await _messageRepository.GetById(id);

            return message.ToModel();
        }

        public async Task<MessageModel> Update(MessageModel entity)
        {
            Message domainModel = entity.ToDomain();

            domainModel.UpdatedDateTime = DateTime.UtcNow;

            Message updatedMessage = await _messageRepository.Update(domainModel);

            return updatedMessage.ToModel();
        }
    }
}
