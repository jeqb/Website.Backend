using Azure;
using Azure.Data.Tables;
using Website.Backend.Domain.Extensions;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.TableStorage;

namespace Website.Backend.Domain.Repositories.Implementations
{
    /// <summary>
    /// Azure Table storage implementation of a Message Repository
    /// </summary>
    public class StorageTableMessageRepository : IRepository<Message>
    {
        private const string _partitionKey = "Message";

        private readonly IStorageTableClient _storageTableClient;

        public StorageTableMessageRepository(IStorageTableClient storageTableClient)
        {
            _storageTableClient = storageTableClient;
        }

        private async Task<long> GetMaxRowKey()
        {
            IEnumerable<Message> entities = await GetAllAsync();

            if (entities.Count() == 0)
            {
                return 0;
            }
            else
            {
                // possibly performance hit over time as size increases.
                Message maxMessage = entities.OrderByDescending(e => e.Id).First();

                return maxMessage.Id;
            }
        }

        public async Task<Message> CreateAsync(Message entity)
        {
            long highestRowKey = await GetMaxRowKey();
            long nextId = highestRowKey + 1;
            entity.Id = nextId;
            entity.CreatedDateTime = DateTime.UtcNow;
            entity.UpdatedDateTime = DateTime.UtcNow;

            TableEntity messageEntity = entity.ToTableEntity();

            Response response = await _storageTableClient.CreateEntityAsync(messageEntity);

            string partitionKey = "Message";
            string rowKey = nextId.ToString();

            IEnumerable<TableEntity> entites = await _storageTableClient.GetEntitiesAsync(partitionKey, rowKey);

            return entites.First().ToMessage();
        }

        public async Task DeleteAsync(Message entity)
        {
            TableEntity messageEntity = entity.ToTableEntity();

            // TODO: decide what to do with Response object
            Response response = await _storageTableClient.DeleteEntityAsync(messageEntity);
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            string partitionKey = "Message";

            IEnumerable<TableEntity> entites = await _storageTableClient.GetEntitiesAsync(partitionKey);

            return entites.Select((entity) => entity.ToMessage());
        }

        public async Task<Message?> GetByIdAsync(string id)
        {
            string partitionKey = "Message";
            string rowKey = id;

            IEnumerable<TableEntity> entites = await _storageTableClient.GetEntitiesAsync(partitionKey, rowKey);

            if (entites.Count() == 0)
            {
                return null;
            }
            else
            {
                return entites.First().ToMessage();
            }
        }

        public async Task UpdateAsync(Message entity)
        {
            TableEntity tableEntity = entity.ToTableEntity();

            await _storageTableClient.UpdateEntityAsync(tableEntity);
        }
    }
}
