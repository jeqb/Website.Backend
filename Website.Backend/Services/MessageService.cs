using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class MessageService : IMessageService
    {
        public MessageService()
        {

        }

        public Task<Message> Create(Message entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Message> Update(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
