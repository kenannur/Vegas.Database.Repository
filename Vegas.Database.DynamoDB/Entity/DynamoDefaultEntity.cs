using System;
using Amazon.DynamoDBv2.DataModel;

namespace Vegas.Database.DynamoDB.Entity
{
    public class DynamoDefaultEntity : IDynamoEntity
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
