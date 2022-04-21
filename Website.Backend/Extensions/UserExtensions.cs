using Website.Backend.Domain;
using Website.Backend.Models;

namespace Website.Backend.Extensions
{
    public static class UserExtensions
    {
        public static User ToModel(this UserDomain domainModel)
        {
            return new User
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
