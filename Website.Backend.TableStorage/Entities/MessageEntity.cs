using Azure;
using Azure.Data.Tables;

namespace Website.Backend.TableStorage.Entities
{
    public class MessageEntity : ITableEntity
    {
        private string _partitionKey { get; set; } = String.Empty;

        private string _rowKey { get; set; } = String.Empty;

        private DateTimeOffset? _timestamp { get; set; }

        private ETag _eTag { get; set; }

        // Some GUID ID
        public string PartitionKey { get => _partitionKey; set => _partitionKey = value; }

        // Email Address
        public string RowKey { get => _rowKey; set => _rowKey = value; }

        public DateTimeOffset? Timestamp { get => _timestamp; set => _timestamp = value; }

        public ETag ETag { get => _eTag; set => _eTag = value; }

        public string Name { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public bool IsRead { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public DateTimeOffset UpdatedDateTime { get; set; }
    }
}
