using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Xunit;
using System;
using Website.Backend.TableStorage.Entities;
using System.Threading.Tasks;
using Azure;
using System.Collections.Generic;

namespace Website.Backend.TableStorage.Test
{
    public class TablesServiceTestSet
    {
        [Fact]
        public async Task Test1()
        {
            // DO NOT COMMIT
            string storageUri = "https://jeqbstorage.table.core.windows.net";
            string tableName = "Users";
            string storageAccountName = "";
            string storageAccountKey = "";

            /*
            var tableClient = new TableClient(
                new Uri(storageUri),
                tableName,
                new TableSharedKeyCredential(storageAccountName, storageAccountKey)
                );
            */

            TableStorageClient tableStorageClient = new TableStorageClient(storageUri, storageAccountName, storageAccountKey);

            MessageEntity messageEntity = new MessageEntity
            {
                PartitionKey = new Guid().ToString(),
                RowKey = "foobar@microsoft.com",
                Name = "DID THIS UPDATE?",
                Content = "This is inserted from a class",
                CreatedDateTime = DateTime.Now,
            };

            // await tableStorageClient.InsertMessageAsync(messageEntity);

            await tableStorageClient.UpdateMessageAsync(messageEntity);

            // await tableStorageClient.DeleteMessage("678e1315-9a6c-4c63-a56c-a83aa5a131dq", "bobsmith@foo.com");

            // IEnumerable<UserEntity> result = await tableStorageClient.GetAllUsersAsync();

            // UserEntity? result = await tableStorageClient.GetUserByEmailAsync("foobar@gmail.com");

            // TableStorageClient target = new TableStorageClient(tableClient);

            // Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>();

            var q = 1;
            /*
            foreach (TableEntity qEntity in queryResultsFilter)
            {
                var y = qEntity.GetString("EmailAddress");

                var z = qEntity.GetString("PasswordHash");
            }
            */

            /*
            UserEntity tesPayload = new UserEntity
            {
                Salt = "dsfsdfs",
                EmailAddress = "sdfsdf",
                PasswordHash = "sdfsdf",
            };

            UserEntity result = await target.InsertUser(tesPayload);
            */



            var x = 1;
        }
    }
}