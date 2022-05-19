using Website.Backend.Domain.Repositories.Interfaces;

namespace Website.Backend.Domain.Repositories.Factories
{
    public interface IRepositoryFactory
    {
        public IUserRepository CreateUserRepository();

        public IRepository<Message> CreateMessageRepository();
    }
}
