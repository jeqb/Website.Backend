using Website.Backend.TableStorage.Entities;

namespace Website.Backend.Domain.Extensions
{
    public static class MessageExtensions
    {
        public static Message ToDomain(this MessageEntity messageEntity)
        {
            return new Message
            {
                Id = Guid.Parse(messageEntity.PartitionKey),
                Email = messageEntity.RowKey,
                Name = messageEntity.Name,
                Content = messageEntity.Content,
                IsRead = messageEntity.IsRead,
                CreatedDateTime = messageEntity.CreatedDateTime,
                UpdatedDateTime = messageEntity.UpdatedDateTime,
            };
        }

        public static MessageEntity ToTableEntity(this Message domainModel)
        {
            return new MessageEntity
            {
                PartitionKey = domainModel.Id.ToString(),
                RowKey = domainModel.Email,
                Name = domainModel.Name,
                Content = domainModel.Content,
                IsRead= domainModel.IsRead,
                CreatedDateTime= domainModel.CreatedDateTime,
                UpdatedDateTime= domainModel.UpdatedDateTime,
            };
        }
    }
}
