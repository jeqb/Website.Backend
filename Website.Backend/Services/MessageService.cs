using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Factories;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Extensions;
using Website.Backend.Infrastructure.Email;
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

        private readonly string _thankYouEmailBody;

        private readonly string _ownerEmailBody;

        public MessageService(ILogger<MessageService> logger, IRepositoryFactory repositoryFactory,
            IEmailNotificationService emailNotificationService, string ownerEmail,
            string thankYouEmailBody, string ownerEmailBody)
        {
            _logger = logger;

            _messageRepository = repositoryFactory.CreateMessageRepository();

            _emailNotificationService = emailNotificationService;

            _ownerEmail = ownerEmail;

            _thankYouEmailBody = thankYouEmailBody;

            _ownerEmailBody = ownerEmailBody;
        }

        public async Task<MessageModel> Create(MessageModel entity)
        {
            Message domainRequest = entity.ToDomain();

            Message createdModel = await _messageRepository.CreateAsync(domainRequest);

            // for reponse to sender
            string toEmail = entity.Email;
            string subject = "Message Received";
            string htmlBody = _thankYouEmailBody;
            htmlBody = htmlBody.Replace("{@Name}", entity.Name);
            htmlBody = htmlBody.Replace("\r\n", string.Empty);

            // owner notification
            string ownerSubject = "New Website Inquiry";
            string ownerBody = _ownerEmailBody;
            ownerBody = ownerBody.Replace("{@Name}", entity.Name);
            ownerBody = ownerBody.Replace("{@Email}", entity.Email);
            ownerBody = ownerBody.Replace("\r\n", string.Empty);

            try
            {
                // email person who sent the message
                await _emailNotificationService.SendEmailAsync(toEmail, subject, htmlBody);

                // email the owner
                await _emailNotificationService.SendEmailAsync(_ownerEmail, ownerSubject, ownerBody);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send email with Exception: {message}", ex.Message);
            }

            return createdModel.ToModel();
        }

        public async Task Delete(string id)
        {
            Message? message = await _messageRepository.GetByIdAsync(id);

            // no message was found
            if (message == null)
            {
                return;
            }
            else
            {
                await _messageRepository.DeleteAsync(message);
            }
        }

        public async Task<IEnumerable<MessageModel>> GetAll()
        {
            IEnumerable<Message> results = await _messageRepository.GetAllAsync();

            return results.Select(
                (message) => message.ToModel()
                );
        }

        public async Task<MessageModel?> GetById(string id)
        {
            Message? message = await _messageRepository.GetByIdAsync(id);

            if (message == null)
            {
                return null;
            }
            else
            {
                return message.ToModel();
            }
        }

        public async Task<MessageModel> Update(MessageModel entity)
        {
            Message domainModel = entity.ToDomain();

            domainModel.UpdatedDateTime = DateTime.UtcNow;

            await _messageRepository.UpdateAsync(domainModel);

            // the null coalescing just makes squigelly go away.
            Message updatedMessage = await _messageRepository.GetByIdAsync(domainModel.Id.ToString()) ?? new();

            return updatedMessage.ToModel();
        }
    }
}
