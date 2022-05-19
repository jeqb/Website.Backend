using Azure.Data.Tables;

namespace Website.Backend.Domain.Extensions
{
    public static class MessageExtensions
    {
        public static Message ToMessage(this TableEntity messageEntity)
        {
            return new Message
            {
                Id = Guid.Parse(messageEntity.RowKey),
                EmailAddress = messageEntity.GetString("EmailAddress"),
                Name = messageEntity.GetString("Name"),
                Content = messageEntity.GetString("Content"),
                IsRead = messageEntity.GetBoolean("IsRead") ?? false,
                CreatedDateTime = messageEntity.GetDateTime("CreatedDateTime"),
                UpdatedDateTime = messageEntity.GetDateTime("UpdatedDateTime"),
            };
        }

        public static TableEntity ToTableEntity(this Message domainModel)
        {
            return new("Message", domainModel.Id.ToString())
            {
                { "EmailAddress", domainModel.EmailAddress },
                { "Name", domainModel.Name },
                { "Content", domainModel.Content },
                { "IsRead", domainModel.IsRead },
                { "CreatedDateTime", domainModel.CreatedDateTime },
                { "UpdatedDateTime", domainModel.UpdatedDateTime },
            };
        }
    }
}
