using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Vegas.Database.MongoDB.Context
{
    public sealed class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _db;
        public MongoDbContext(IMongoDatabase db) => _db = db;

        public IMongoCollection<BsonDocument> Collection(string collectionName)
            => _db.GetCollection<BsonDocument>(collectionName);

        public IMongoCollection<TEntity> Collection<TEntity>()
            => _db.GetCollection<TEntity>(typeof(TEntity).Name);

        public IMongoQueryable<TEntity> AsMongoQueryable<TEntity>()
            => Collection<TEntity>().AsQueryable();

        public IQueryable<TEntity> AsQueryable<TEntity>()
            => AsMongoQueryable<TEntity>().AsQueryable();
    }
}
