using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Website.Backend.TableStorage.Test
{
    public class StorageTableClientTests
    {
        private readonly string _storageUri;

        private readonly string _storageAccountName;

        private readonly string _storageAccountKey;

        private readonly string _tableName;

        private readonly IConfiguration _configuration;

        public StorageTableClientTests()
        {
            // TODO: use appsettings.json
        }

        [Fact]
        public async Task CreateEntityAsync_ReturnsFalseIsError_GivenValidTableEntity()
        {
            // Arrange

            StorageTableClient target = new(_storageUri, _storageAccountName, _storageAccountKey, _tableName);

            string partitionKey = "Message";
            string rowKey = Guid.NewGuid().ToString();

            TableEntity testPayload = new(partitionKey, rowKey)
            {
                { "EmailAddress", "unittest@test.com" },
                { "CreatedDate", DateTime.UtcNow },
                { "UpdatedDate", DateTime.UtcNow },
                { "Name", "UNIT TEST" },
                { "Content", "THIS IS FROM A UNIT TEST" },
                { "IsRead", true },
            };


            // Act

            Response result = await target.CreateEntityAsync(testPayload);


            // Assert

            Assert.False(result.IsError);
        }

        /// <summary>
        /// This assumes that there is a Column in the table called "EmailAddress" which is a string.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetEntitiesAsync_ReturnsNonNullEmailAddress_GivenMessagePartitionKey()
        {
            // Arrange

            StorageTableClient target = new(_storageUri, _storageAccountName, _storageAccountKey, _tableName);

            string partitionKey = "Message";


            // Act

            IEnumerable<TableEntity> entities = await target.GetEntitiesAsync(partitionKey);
            IEnumerable<string> emailAddresses = entities
                                                    .Where((e) =>
                                                    {
                                                        bool hasValue = e.TryGetValue("EmailAddress", out object email);

                                                        return hasValue;
                                                    })
                                                    .Select((e) =>
                                                    {
                                                        var hasValue = e.TryGetValue("EmailAddress", out object email);

                                                        return (string)email;
                                                    })
                                                    .ToList();


            // Assert

            Assert.NotEmpty(emailAddresses);
        }

        /// <summary>
        /// This is really just testing whether or not providing the RowKey filters the results down
        /// PartitionKey + RowKey should be the primary key so we should get a single item in the array.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetEntitiesAsync_ReturnsSizeOneArray_GivenMessagePartitionKeyAndRowKey()
        {
            // Arrange

            StorageTableClient target = new(_storageUri, _storageAccountName, _storageAccountKey, _tableName);

            // these currently live in the DB
            string partitionKey = "Message";
            string rowKey = Guid.Parse("0acf3b69-8f40-4e69-a010-e07c7a7d4d30").ToString();


            // Act

            IEnumerable<TableEntity> data = await target.GetEntitiesAsync(partitionKey, rowKey);
            int resultCount = data.Count();


            // Assert

            Assert.Equal(1, resultCount);
        }

        [Fact]
        public async Task DeleteEntityAsync_DeletesEntity_GivenEntity()
        {
            // Arrange

            StorageTableClient target = new(_storageUri, _storageAccountName, _storageAccountKey, _tableName);

            TableEntity deleteMe = new("UnitTest", Guid.NewGuid().ToString());
            // create something to delete first
            Response creationResult = await target.CreateEntityAsync(deleteMe);


            // Act

            Response deleteResult = await target.DeleteEntityAsync(deleteMe);


            // Assert

            Assert.False(deleteResult.IsError);
        }

        [Fact]
        public async Task UpdateEntityAsync_UpdatesEmailProperty_GivenNewValue()
        {
            // Arrange

            StorageTableClient target = new(_storageUri, _storageAccountName, _storageAccountKey, _tableName);

            string partitionKey = "UnitTest";
            string rowKey = Guid.NewGuid().ToString();
            string oldEmailAddressColumn = "OldEmail@foo.com";
            string newEmailAddressColumn = "please be updated";

            TableEntity createMe = new(partitionKey, rowKey)
            {
                { "EmailAddress", oldEmailAddressColumn }
            };

            TableEntity updateMe = new(partitionKey, rowKey)
            {
                { "EmailAddress", newEmailAddressColumn }
            };


            // Act

            // create something to delete first
            Response creationResult = await target.CreateEntityAsync(createMe);

            Response updateResult = await target.UpdateEntityAsync(updateMe);

            IEnumerable<TableEntity> checkThisEntity = await target.GetEntitiesAsync(partitionKey, rowKey);

            TableEntity result = checkThisEntity.FirstOrDefault() ?? new("", "");

            result.TryGetValue("EmailAddress", out object hopefullyUpdatedValue);

            string checkMe = "";
            if (hopefullyUpdatedValue != null)
            {
                checkMe = hopefullyUpdatedValue.ToString();
            }


            // Assert

            Assert.Equal(newEmailAddressColumn, checkMe);
        }

        [Fact]
        public async Task GetEntitiesWithCustomQueryAsync_RetrievesEntity_GivenSomeColumn()
        {
            // Arrange

            StorageTableClient target = new(_storageUri, _storageAccountName, _storageAccountKey, _tableName);

            string emailAddress = "some_email@google.com";

            string customQuery = $"PartitionKey eq 'User' and EmailAddress eq '{emailAddress}'";


            // Act

            IEnumerable<TableEntity> result = await target.GetEntitiesWithCustomQueryAsync(customQuery);


            // Assert

            Assert.NotEmpty(result);
        }
    }
}
