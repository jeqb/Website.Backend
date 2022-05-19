using Azure.Data.Tables;

namespace Website.Backend.Domain.Extensions
{
    public static class UserExtensions
    {
        public static User ToUser(this TableEntity userEntity)
        {
            return new User
            {
                Id = Guid.Parse(userEntity.RowKey),
                EmailAddress = userEntity.GetString("EmailAddress"),
                PasswordHash = userEntity.GetString("PasswordHash"),
                Salt = userEntity.GetString("Salt"),
                CreatedDateTime = userEntity.GetDateTime("CreatedDateTime"),
                UpdatedDateTime = userEntity.GetDateTime("UpdatedDateTime"),
            };
        }

        public static TableEntity ToTableEntity(this User domainModel)
        {
            return new("User", domainModel.Id.ToString())
            {
                { "EmailAddress", domainModel.EmailAddress },
                { "PasswordHash", domainModel.PasswordHash },
                { "Salt", domainModel.Salt },
                { "CreatedDateTime", domainModel.CreatedDateTime },
                { "UpdatedDateTime", domainModel.UpdatedDateTime },
            };
        }
    }
}
