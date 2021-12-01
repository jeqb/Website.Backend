using Website.Backend.Models;

namespace Website.Backend.Repositories
{
    public interface IRepositoryFactory
    {
        public IRepository<User> CreateUserRepository();

        public IRepository<Message> CreateMessageRepository();
    }
}
