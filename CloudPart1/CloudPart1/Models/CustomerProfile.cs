using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace CloudPart1.Models
{
    public class CustomerProfile:ITableEntity
    {
        [Key]

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfileId { get; set; }

        
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public ETag ETag { get; set; }= ETag.All;
        public DateTimeOffset? Timestamp { get; set; }
    }
}
