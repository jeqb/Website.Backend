using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.TableStorage;

namespace Website.Backend.Domain.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly string _storageUri;

        private readonly string _storageAccountName;

        private readonly string _storageAccountKey;

        private readonly TableStorageClient _tableStorageClient;

        public RepositoryFactory(string storageUri, string storageAccountName, string storageAccountKey)
        {
            _storageUri = storageUri;

            _storageAccountName = storageAccountName;

            _storageAccountKey = storageAccountKey;

            _tableStorageClient = new TableStorageClient(_storageUri, _storageAccountName, _storageAccountKey);
        }

        public IRepository<Message> CreateMessageRepository()
        {
            return new MessageRepository(_tableStorageClient);
        }

        public IUserRepository CreateUserRepository()
        {
            return new UserRepository();
        }
    }
}
