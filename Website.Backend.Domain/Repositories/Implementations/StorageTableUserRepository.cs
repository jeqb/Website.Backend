using Azure;
using Azure.Data.Tables;
using Website.Backend.Domain.Extensions;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.TableStorage;

namespace Website.Backend.Domain.Repositories.Implementations
{
    public class StorageTableUserRepository : IUserRepository
    {
        private const string _partitionKey = "User";

        private readonly IStorageTableClient _storageTableClient;

        public StorageTableUserRepository(IStorageTableClient storageTableClient)
        {
            _storageTableClient = storageTableClient;
        }

        private async Task<long> GetMaxRowKeyAsync()
        {
            IEnumerable<User> entities = await GetAllAsync();

            if (entities.Count() == 0)
            {
                return 0;
            }
            else
            {
                // possibly performance hit over time as size increases.
                User maxUser = entities.OrderByDescending(e => e.Id).First();

                return maxUser.Id;
            }
        }

        public async Task<User> CreateAsync(User entity)
        {
            long highestRowKey = await GetMaxRowKeyAsync();
            long nextId = highestRowKey + 1;
            entity.Id = nextId; ;
            entity.CreatedDateTime = DateTime.UtcNow;
            entity.UpdatedDateTime = DateTime.UtcNow;

            TableEntity userEntity = entity.ToTableEntity();

            Response response = await _storageTableClient.CreateEntityAsync(userEntity);

            string partitionKey = "Message";
            string rowKey = nextId.ToString();

            IEnumerable<TableEntity> entites = await _storageTableClient.GetEntitiesAsync(partitionKey, rowKey);

            return entites.First().ToUser();
        }

        public async Task DeleteAsync(User entity)
        {
            TableEntity messageEntity = entity.ToTableEntity();

            // TODO: decide what to do with Response object
            Response response = await _storageTableClient.DeleteEntityAsync(messageEntity);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string partitionKey = "User";

            IEnumerable<TableEntity> entites = await _storageTableClient.GetEntitiesAsync(partitionKey);

            return entites.Select((entity) => entity.ToUser());
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            string partitionKey = "User";
            string rowKey = id;

            IEnumerable<TableEntity> entites = await _storageTableClient.GetEntitiesAsync(partitionKey, rowKey);

            if (entites.Count() == 0)
            {
                return null;
            }
            else
            {
                return entites.First().ToUser();
            }
        }

        public async Task UpdateAsync(User entity)
        {
            TableEntity tableEntity = entity.ToTableEntity();

            // TODO: decide what to do with Response object
            Response response = await _storageTableClient.UpdateEntityAsync(tableEntity);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            string customQuery = $"PartitionKey eq 'User' and EmailAddress eq '{email}'";

            IEnumerable<TableEntity> entites =
                await _storageTableClient.GetEntitiesWithCustomQueryAsync(customQuery);

            if (entites.Count() == 0)
            {
                return null;
            }
            else
            {
                return entites.First().ToUser();
            }
        }
    }
}
