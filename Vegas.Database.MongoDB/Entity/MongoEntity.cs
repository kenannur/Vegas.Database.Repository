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
        /// <summary>
        /// Default Mongo primary key type
        /// </summary>
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// BsonDateTimeOptions(Kind = DateTimeKind.Local) -> DB'de UTC olarak tutulacak.
        /// Deserialize ederken local time'a otomatik çevrilecek
        /// </summary>
        [BsonIgnoreIfNull, BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreatedDate { get; set; }
    }
}
