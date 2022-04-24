using Azure;
using Azure.Data.Tables;
using Website.Backend.TableStorage.Entities;
using System.Net;

namespace Website.Backend.TableStorage
{
    public class TableStorageClient
    {

        #region Private Properties

        private const string _usersTable = "Users";

        private readonly string _storageUri;

        private readonly string _storageAccountName;

        private readonly string _storageAccountKey;

        #endregion

        #region Private Methods

        private TableClient _createTableClient(string tableName)
        {
            return new TableClient(
                new Uri(_storageUri),
                tableName,
                new TableSharedKeyCredential(_storageAccountName, _storageAccountKey)
                );
        }

        #endregion

        #region Constructor

        public TableStorageClient(string storageUri, string storageAccountName, string storageAccountKey)
        {
            _storageUri = storageUri;

            _storageAccountName = storageAccountName;

            _storageAccountKey = storageAccountKey;
        }

        #endregion

        #region User Table Methods

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            TableClient userTableClient = _createTableClient(_usersTable);

            AsyncPageable<UserEntity> queryResultsFilter = userTableClient.QueryAsync<UserEntity>();

            List<UserEntity> users = new();

            await foreach (UserEntity user in queryResultsFilter)
            {
                users.Add(user);
            }

            return users;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            TableClient userTableClient = _createTableClient(_usersTable);

            AsyncPageable<UserEntity> queryResultsFilter = userTableClient.QueryAsync<UserEntity>(
                user => user.PartitionKey == email
                );

            List<UserEntity> users = new();

            await foreach(UserEntity user in queryResultsFilter)
            {
                users.Add(user);
            }

            return users.FirstOrDefault();
        }

        public async Task InsertUserAsync(UserEntity user)
        {
            TableClient userTableClient = _createTableClient(_usersTable);

            await userTableClient.AddEntityAsync(user);
        }

        #endregion
    }
}