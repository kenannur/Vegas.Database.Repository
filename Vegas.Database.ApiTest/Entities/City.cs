using Amazon.DynamoDBv2.DataModel;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.ApiTest.Entities
{
    public class City : DynamoCustomEntity
    {
        [DynamoDBRangeKey]
        public string Name { get; set; }

        [DynamoDBHashKey]
        public string BaseId { get; set; }
    }
}
