namespace Website.Backend.Domain.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        public IUserRepository CreateUserRepository();

        public IRepository<MessageDomain> CreateMessageRepository();
    }
}
