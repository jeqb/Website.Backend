using Website.Backend.Models;

namespace Website.Backend.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<IEnumerable<Message>> GetAll();

        public Task<Message> GetById(int id);

        public Task<Message> Create(Message entity);

        public Task<Message> Update(Message entity);

        public Task Delete(int id);
    }
}
