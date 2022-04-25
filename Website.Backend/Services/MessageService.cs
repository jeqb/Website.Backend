using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Extensions;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _messageRepository;

        public MessageService(IRepositoryFactory repositoryFactory)
        {
            _messageRepository = repositoryFactory.CreateMessageRepository();
        }

        public async Task<MessageModel> Create(MessageModel entity)
        {
            Message createdModel = await _messageRepository.Create(entity.ToDomain());

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
