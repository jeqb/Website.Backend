using Website.Backend.Domain.Repositories.Interfaces;

namespace Website.Backend.Domain.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        // TODO: pass the ability to talk to the database in here.
        // TODO: Add method to specify an Azure Table Store repo vs SQL repo
        public RepositoryFactory()
        {

        }

        public IRepository<Message> CreateMessageRepository()
        {
            return new MessageRepository();
        }

        public IUserRepository CreateUserRepository()
        {
            return new UserRepository();
        }
    }
}
