using Azure.Data.Tables;

namespace Website.Backend.Domain.Extensions
{
    public static class UserExtensions
    {
        public static User ToUser(this TableEntity userEntity)
        {
            long Id = long.Parse(userEntity.RowKey);
            string EmailAddress = userEntity.GetString("EmailAddress");
            string PasswordHash = userEntity.GetString("PasswordHash");
            string Salt = userEntity.GetString("Salt");
            DateTime? CreatedDateTime = userEntity.GetDateTime("CreatedDateTime");
            DateTime? UpdatedDateTime = userEntity.GetDateTime("UpdatedDateTime");

            return new User
            {
                Id = Id,
                EmailAddress = EmailAddress,
                PasswordHash = PasswordHash,
                Salt = Salt,
                CreatedDateTime = CreatedDateTime,
                UpdatedDateTime = UpdatedDateTime
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
