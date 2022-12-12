using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Vegas.Database.MongoDB.Context;

public interface IMongoDbContext
{
    IMongoCollection<BsonDocument> Collection(string collectionName);

    IMongoCollection<TEntity> Collection<TEntity>();

    IMongoQueryable<TEntity> AsMongoQueryable<TEntity>();

    IQueryable<TEntity> AsQueryable<TEntity>();
}

