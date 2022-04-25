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
            // await _messageRepository.Delete(id);
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
