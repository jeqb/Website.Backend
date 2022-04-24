using Azure;
using Azure.Data.Tables;

namespace Website.Backend.TableStorage.Entities
{
    public class UserEntity : ITableEntity
    {
        private string _partitionKey {  get; set; } = String.Empty;

        private string _rowKey { get; set; } = String.Empty;

        private DateTimeOffset? _timestamp { get; set; }

        private ETag _eTag { get; set; }

        // Email Address
        public string PartitionKey { get => _partitionKey; set => _partitionKey = value; }
        
        // Some GUID ID
        public string RowKey { get => _rowKey; set => _rowKey = value; }

        public DateTimeOffset? Timestamp { get => _timestamp; set => _timestamp = value; }

        public ETag ETag { get => _eTag; set => _eTag = value; }

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;
    }
}
