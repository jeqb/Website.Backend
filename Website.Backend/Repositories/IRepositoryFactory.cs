using Website.Backend.Models;

namespace Website.Backend.Repositories
{
    public interface IRepositoryFactory
    {
        public IUserRepository CreateUserRepository();

        public IRepository<Message> CreateMessageRepository();
    }
}
