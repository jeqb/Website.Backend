using Website.Backend.Domain;
using Website.Backend.Models;

namespace Website.Backend.Extensions
{
    public static class MessageExtensions
    {
        public static MessageModel ToModel(this Message domainModel)
        {
            return new MessageModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Email = domainModel.EmailAddress,
                Content = domainModel.Content,
                Created = domainModel.CreatedDateTime,
            };
        }

        public static Message ToDomain(this MessageModel model)
        {
            return new Message
            {
                Id = model.Id,
                Name = model.Name,
                EmailAddress = model.Email,
                Content = model.Content,
                CreatedDateTime = model.Created,
            };
        }
    }
}
