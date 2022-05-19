using Azure;
using Azure.Data.Tables;

namespace Website.Backend.TableStorage
{
    public interface IStorageTableClient
    {
        public Task<Response> CreateEntityAsync(TableEntity insertMe);

        public Task<IEnumerable<TableEntity>> GetEntitiesAsync(string partitionKey, string? rowKey = null);

        public Task<IEnumerable<TableEntity>> GetEntitiesWithCustomQueryAsync(string customQuery);

        public Task<Response> UpdateEntityAsync(TableEntity updateMe);

        public Task<Response> DeleteEntityAsync(TableEntity deleteMe);
    }
}
