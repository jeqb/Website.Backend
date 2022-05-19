using Azure;
using Azure.Data.Tables;

namespace Website.Backend.TableStorage
{
    /// <summary>
    /// Reusable Class to insert do CRUD functionality on a given table using any TableEntity
    /// </summary>
    public class StorageTableClient : IStorageTableClient
    {
        private readonly string _storageUri;

        private readonly string _storageAccountName;

        private readonly string _storageAccountKey;

        private readonly string _tableName;

        private readonly TableClient _tableClient;

        public StorageTableClient(string storageUri, string storageAccountName, string storageAccountKey,
            string tableName)
        {
            _storageUri = storageUri;

            _storageAccountName = storageAccountName;

            _storageAccountKey = storageAccountKey;

            _tableName = tableName;

            _tableClient = new TableClient(new Uri(_storageUri), _tableName,
                new TableSharedKeyCredential(_storageAccountName, _storageAccountKey));
        }

        public async Task<Response> CreateEntityAsync(TableEntity insertMe)
        {
            return await _tableClient.AddEntityAsync(insertMe);
        }

        public async Task<IEnumerable<TableEntity>> GetEntitiesAsync(string partitionKey, string? rowKey = null)
        {
            List<TableEntity> results = new();

            string filterQUery = string.IsNullOrEmpty(rowKey) ? $"PartitionKey eq '{partitionKey}'"
                        : $"PartitionKey eq '{partitionKey}' and RowKey eq '{rowKey}'";

            AsyncPageable<TableEntity> queryResultsFiler = _tableClient.QueryAsync<TableEntity>(filter: filterQUery);

            await foreach (TableEntity tableEntity in queryResultsFiler)
            {
                results.Add(tableEntity);
            }

            return results;
        }

        public async Task<IEnumerable<TableEntity>> GetEntitiesWithCustomQueryAsync(string customQuery)
        {
            List<TableEntity> results = new();

            AsyncPageable<TableEntity> queryResultsFiler = _tableClient.QueryAsync<TableEntity>(filter: customQuery);

            await foreach (TableEntity tableEntity in queryResultsFiler)
            {
                results.Add(tableEntity);
            }

            return results;
        }

        public async Task<Response> DeleteEntityAsync(TableEntity deleteMe)
        {
            return await _tableClient.DeleteEntityAsync(deleteMe.PartitionKey, deleteMe.RowKey);
        }

        public async Task<Response> UpdateEntityAsync(TableEntity updateMe)
        {
            string rowKey = updateMe.RowKey;
            string partitionKey = updateMe.PartitionKey;

            // just need the Etag
            Response<TableEntity> queryResult = await _tableClient.GetEntityAsync<TableEntity>(partitionKey, rowKey);
            TableEntity foundEntity = queryResult.Value;
            ETag eTag = foundEntity.ETag;

            return await _tableClient.UpdateEntityAsync(updateMe, eTag);
        }
    }
}
