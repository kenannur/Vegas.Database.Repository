using System.Linq;
using MongoDB.Driver;

namespace Vegas.Database.Mongo.Context
{
    public sealed class MongoDbContext
    {
        private readonly IMongoDatabase _db;
        public MongoDbContext(IMongoDatabase db) => _db = db;

        public IMongoCollection<TEntity> Collection<TEntity>() => _db.GetCollection<TEntity>(typeof(TEntity).Name);
        public IQueryable<TEntity> AsQueryable<TEntity>() => Collection<TEntity>().AsQueryable();
    }
}
