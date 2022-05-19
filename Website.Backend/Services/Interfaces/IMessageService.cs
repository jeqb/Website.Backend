using Website.Backend.Models;

namespace Website.Backend.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<IEnumerable<MessageModel>> GetAll();

        public Task<MessageModel?> GetById(string id);

        public Task<MessageModel> Create(MessageModel entity);

        public Task<MessageModel> Update(MessageModel entity);

        public Task Delete(string id);
    }
}
