using Website.Backend.Domain.Repositories.Interfaces;

namespace Website.Backend.Domain.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        // TODO: inject database connection here.
        public MessageRepository()
        {

        }

        public async Task<Message> Create(Message entity)
        {
            Task.Yield();

            return entity;
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return new List<Message>
            {
                new Message
                {
                    Id = Guid.NewGuid(),
                    Name = "Joe",
                    Email = "joe@foo.com",
                    Content = "YOu SUCK!!!",
                    CreatedDateTime = DateTime.Now,
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    Name = "Bob",
                    Email = "bob@foo.com",
                    Content = "YOu SUCK!!!",
                    CreatedDateTime = DateTime.Now,
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    Name = "Billy",
                    Email = "billy@foo.com",
                    Content = "YOu SUCK!!!",
                    CreatedDateTime = DateTime.Now,
                }
            };
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
