using Website.Backend.Models;

namespace Website.Backend.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        // TODO: pass the ability to talk to the database in here.
        public RepositoryFactory()
        {

        }

        public IRepository<User> CreateUserRepository()
        {
            return new UserRepository();
        }
    }
}
