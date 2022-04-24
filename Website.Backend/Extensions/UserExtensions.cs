using Website.Backend.Domain;
using Website.Backend.Models;

namespace Website.Backend.Extensions
{
    public static class UserExtensions
    {
        public static UserModel ToModel(this User domainModel)
        {
            return new UserModel
            {
                Id = domainModel.Id,
                EmailAddress = domainModel.EmailAddress,
                PasswordHash = domainModel.PasswordHash,
                Salt = domainModel.Salt,
                CreatedDate = domainModel.CreatedDate,
                UpdatedDate = domainModel.UpdatedDate,
            };
        }
    }
}
