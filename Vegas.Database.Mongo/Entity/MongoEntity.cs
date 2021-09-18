using System;
using MongoDB.Bson.Serialization.Attributes;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.Mongo.Entity
{
    public abstract class MongoEntity : IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }

        /// <summary>
        /// BsonDateTimeOptions(Kind = DateTimeKind.Local) -> DB'de UTC olarak tutulacak.
        /// Deserialize ederken local time'a otomatik çevrilecek
        /// </summary>
        [BsonIgnoreIfNull, BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreatedDate { get; set; }
    }
}
