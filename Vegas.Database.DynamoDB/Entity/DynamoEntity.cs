using System;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.DynamoDB.Entity
{
    public class DynamoEntity : IEntity<string>
    {
        public string Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
