using System;

namespace Vegas.Database.DynamoDB.Entity
{
    public class DynamoCustomEntity : IDynamoEntity
    {
        public string Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
