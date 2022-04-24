using Website.Backend.Domain;
using Website.Backend.Models;

namespace Website.Backend.Extensions
{
    public static class MessageExtensions
    {
        public static MessageModel ToModel(this MessageDomain domainModel)
        {
            return new MessageModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Email = domainModel.Email,
                Content = domainModel.Content,
                Created = domainModel.Created,
            };
        }

        public static MessageDomain ToDomain(this MessageModel model)
        {
            return new MessageDomain
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Content = model.Content,
                Created = model.Created,
            };
        }
    }
}
