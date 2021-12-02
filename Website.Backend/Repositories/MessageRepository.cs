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
            return new List<Message>
            {
                new Message
                {
                    Id = 1,
                    Name = "Joe",
                    Email = "joe@foo.com",
                    Content = "YOu SUCK!!!",
                    Created = DateTime.Now,
                },
                new Message
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@foo.com",
                    Content = "YOu SUCK!!!",
                    Created = DateTime.Now,
                },
                new Message
                {
                    Id = 3,
                    Name = "Billy",
                    Email = "billy@foo.com",
                    Content = "YOu SUCK!!!",
                    Created = DateTime.Now,
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
