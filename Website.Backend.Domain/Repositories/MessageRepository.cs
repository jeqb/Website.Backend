using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.TableStorage;
using Website.Backend.TableStorage.Entities;
using Website.Backend.Domain.Extensions;

namespace Website.Backend.Domain.Repositories
{
    /// <summary>
    /// this is tightly coupled to Azure Table Storage for now :(
    /// </summary>
    public class MessageRepository : IRepository<Message>
    {
        private readonly TableStorageClient _tableStorageClient;

        public MessageRepository(TableStorageClient tableStorageClient)
        {
            _tableStorageClient = tableStorageClient;
        }

        public async Task<Message> Create(Message entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDateTime = DateTime.UtcNow;
            entity.UpdatedDateTime = DateTime.UtcNow;

            MessageEntity messageEntity = entity.ToTableEntity();

            await _tableStorageClient.InsertMessageAsync(messageEntity);

            return entity;
        }

        public async Task Delete(Message entity)
        {
            MessageEntity messageEntity = entity.ToTableEntity();

            await _tableStorageClient.DeleteMessage(messageEntity.PartitionKey, messageEntity.RowKey);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            IEnumerable<MessageEntity> messageEntities = await _tableStorageClient.GetAllMessagesAsync();

            messageEntities = messageEntities.OrderByDescending(x => x.CreatedDateTime).ToList();

            return messageEntities.Select(
                (message) => message.ToDomain()
                );
        }

        public async Task<Message> GetById(Guid id)
        {
            MessageEntity messageEntity = await _tableStorageClient.GetMessageById(id);

            if (messageEntity == null)
            {
                return new Message();
            }
            else
            {
                return messageEntity.ToDomain();
            }
        }

        public async Task<Message> Update(Message entity)
        {
            MessageEntity messageEntity = entity.ToTableEntity();

            await _tableStorageClient.UpdateMessageAsync(messageEntity);

            return messageEntity.ToDomain();
        }
    }
}
