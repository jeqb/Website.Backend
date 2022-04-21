using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Extensions;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<MessageDomain> _messageRepository;

        public MessageService(IRepositoryFactory repositoryFactory)
        {
            _messageRepository = repositoryFactory.CreateMessageRepository();
        }

        public async Task<Message> Create(Message entity)
        {
            MessageDomain createdModel = await _messageRepository.Create(entity.ToDomain());

            return createdModel.ToModel();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            IEnumerable<MessageDomain> results = await _messageRepository.GetAll();

            return results.Select(
                (message) => message.ToModel()
                );
        }

        public async Task<Message> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Message> Update(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
