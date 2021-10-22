﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Vegas.Database.MongoDB.Entity
{
    public abstract class MongoEntity : IMongoEntity
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
