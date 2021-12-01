using Website.Backend.Models;

namespace Website.Backend.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        // TODO: inject database connection here.
        public MessageRepository()
        {

        }

        public async Task<Message> Create(Message entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            throw new NotImplementedException();
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
