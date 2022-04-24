using Website.Backend.TableStorage.Entities;

namespace Website.Backend.Domain.Extensions
{
    public static class UserExtensions
    {
        public static User ToDomain(this UserEntity userEntity)
        {
            return new User
            {
                Id = Guid.Parse(userEntity.RowKey),
                EmailAddress = userEntity.PartitionKey,
                PasswordHash = userEntity.PasswordHash,
                Salt = userEntity.Salt,
                CreatedDateTime = userEntity.CreatedDateTime,
                UpdatedDateTime = userEntity.UpdatedDateTime,
            };
        }

        public static UserEntity ToTableEntity(this User domainModel)
        {
            return new UserEntity
            {
                RowKey = domainModel.Id.ToString(),
                PartitionKey = domainModel.EmailAddress,
                PasswordHash = domainModel.PasswordHash,
                Salt = domainModel.Salt,
                CreatedDateTime = domainModel.CreatedDateTime,
                UpdatedDateTime = domainModel.UpdatedDateTime,
            };
        }
    }
}
