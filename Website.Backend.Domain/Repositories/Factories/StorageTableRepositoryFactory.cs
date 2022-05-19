using Website.Backend.Domain.Repositories.Implementations;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.TableStorage;

namespace Website.Backend.Domain.Repositories.Factories
{
    /// <summary>
    /// Repository Factory that Returns repositories talking to the Azure Storage Table
    /// </summary>
    public class StorageTableRepositoryFactory : IRepositoryFactory
    {
        private readonly string _storageUri;

        private readonly string _storageAccountName;

        private readonly string _storageAccountKey;

        private readonly string _tableName;

        private readonly IStorageTableClient _storageTableClient;

        public StorageTableRepositoryFactory(string storageUri, string storageAccountName,
            string storageAccountKey, string tableName)
        {
            _storageUri = storageUri;

            _storageAccountName = storageAccountName;

            _storageAccountKey = storageAccountKey;

            _tableName = tableName;

            _storageTableClient = new StorageTableClient(_storageUri, _storageAccountName,
                _storageAccountKey, _tableName);
        }

        public IRepository<Message> CreateMessageRepository()
        {
            return new StorageTableMessageRepository(_storageTableClient);
        }

        public IUserRepository CreateUserRepository()
        {
            return new StorageTableUserRepository(_storageTableClient);
        }
    }
}
