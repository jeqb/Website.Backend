using Website.Backend.Domain.Repositories.Interfaces;

namespace Website.Backend.Domain.Repositories
{
    public class MessageRepository : IRepository<MessageDomain>
    {
        // TODO: inject database connection here.
        public MessageRepository()
        {

        }

        public async Task<MessageDomain> Create(MessageDomain entity)
        {
            Task.Yield();

            return entity;
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MessageDomain>> GetAll()
        {
            return new List<MessageDomain>
            {
                new MessageDomain
                {
                    Id = 1,
                    Name = "Joe",
                    Email = "joe@foo.com",
                    Content = "YOu SUCK!!!",
                    Created = DateTime.Now,
                },
                new MessageDomain
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@foo.com",
                    Content = "YOu SUCK!!!",
                    Created = DateTime.Now,
                },
                new MessageDomain
                {
                    Id = 3,
                    Name = "Billy",
                    Email = "billy@foo.com",
                    Content = "YOu SUCK!!!",
                    Created = DateTime.Now,
                }
            };
        }

        public async Task<MessageDomain> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageDomain> Update(MessageDomain entity)
        {
            throw new NotImplementedException();
        }
    }
}
