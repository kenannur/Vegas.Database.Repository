using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.MongoDB.Entity
{
    public interface IMongoEntity : IEntity<string>
    { }

    public abstract class MongoEntity : IMongoEntity
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfNull, BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? CreatedDate { get; set; }
    }
}
