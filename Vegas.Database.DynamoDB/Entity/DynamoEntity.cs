using System;
using Amazon.DynamoDBv2.DataModel;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.DynamoDB.Entity
{
    public interface IDynamoEntity : IEntity<Guid>
    { }

    public abstract class DynamoDefaultEntity : IDynamoEntity
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

    public abstract class DynamoCustomEntity : IDynamoEntity
    {
        public Guid Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
