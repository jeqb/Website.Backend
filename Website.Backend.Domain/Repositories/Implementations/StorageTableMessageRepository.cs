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

        public async Task<Message> CreateAsync(Message entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDateTime = DateTime.UtcNow;
            entity.UpdatedDateTime = DateTime.UtcNow;

            TableEntity messageEntity = entity.ToTableEntity();

            // TODO: decide what to do with Response object
            // possibly just a task?
            Response response = await _storageTableClient.CreateEntityAsync(messageEntity);

            return entity;
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

        public async Task<Message?> GetByIdAsync(Guid id)
        {
            string partitionKey = "Message";
            string rowKey = id.ToString();

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
