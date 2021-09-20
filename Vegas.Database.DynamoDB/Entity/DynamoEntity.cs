using System;
using Amazon.DynamoDBv2.DataModel;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.DynamoDB.Entity
{
    public class DynamoEntity : IEntity<string>
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
