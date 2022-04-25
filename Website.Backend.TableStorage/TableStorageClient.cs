using Azure;
using Azure.Data.Tables;
using Website.Backend.TableStorage.Entities;

namespace Website.Backend.TableStorage
{
    public class TableStorageClient
    {

        #region Private Properties

        private const string _usersTable = "Users";

        private const string _messagesTable = "Messages";

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

        /// <summary>
        /// retrieves all users from the User table.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// retrieves a user by their email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            TableClient userTableClient = _createTableClient(_usersTable);

            AsyncPageable<UserEntity> queryResultsFilter = userTableClient.QueryAsync<UserEntity>(
                user => user.PartitionKey == email
                );

            List<UserEntity> users = new();

            await foreach (UserEntity user in queryResultsFilter)
            {
                users.Add(user);
            }

            return users.FirstOrDefault();
        }

        /// <summary>
        /// Inserts a user entity into the User table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task InsertUserAsync(UserEntity user)
        {
            TableClient userTableClient = _createTableClient(_usersTable);

            Response result = await userTableClient.AddEntityAsync(user);

            // TODO: can we get the inserted object back?
        }

        #endregion

        #region Message Table Methods

        /// <summary>
        /// Get all messages from the message table.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MessageEntity>> GetAllMessagesAsync()
        {
            TableClient messageTableClient = _createTableClient(_messagesTable);

            AsyncPageable<MessageEntity> queryResultsFilter = messageTableClient.QueryAsync<MessageEntity>();

            List<MessageEntity> messages = new();

            await foreach (MessageEntity message in queryResultsFilter)
            {
                messages.Add(message);
            }

            return messages;
        }

        public async Task<MessageEntity?> GetMessageById(Guid partitionKey)
        {
            TableClient messageTableClient = _createTableClient(_messagesTable);

            AsyncPageable<MessageEntity> queryResultsFilter = messageTableClient.QueryAsync<MessageEntity>(
                message => message.PartitionKey == partitionKey.ToString()
                );

            List<MessageEntity> messages = new();

            await foreach (MessageEntity message in queryResultsFilter)
            {
                messages.Add(message);
            }

            // TODO: something about the nullability. probably make the return value
            // on the interface nullable.
            return messages.FirstOrDefault();
        }

        /// <summary>
        /// Delete a message using it's PartitionKey and RowKey
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public async Task DeleteMessage(string partitionKey, string rowKey)
        {
            TableClient messageTableClient = _createTableClient(_messagesTable);

            Response result = await messageTableClient.DeleteEntityAsync(partitionKey, rowKey);

            // TODO: can we get the inserted object back?
        }

        /// <summary>
        /// Insert a message into the Messages table
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task InsertMessageAsync(MessageEntity message)
        {
            TableClient messageTableClient = _createTableClient(_messagesTable);

            Response result = await messageTableClient.AddEntityAsync(message);

            // TODO: can we get the inserted object back?
        }

        /// <summary>
        /// Update a Message in the Messages tables
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task UpdateMessageAsync(MessageEntity message)
        {
            TableClient messageTableClient = _createTableClient(_messagesTable);

            Response result = await messageTableClient.UpdateEntityAsync(message, ETag.All, TableUpdateMode.Replace);

            // TODO: can we get the inserted object back?
        }

        #endregion
    }
}
