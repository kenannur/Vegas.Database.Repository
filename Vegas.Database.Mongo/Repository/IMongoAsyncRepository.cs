using Vegas.Database.Abstraction.Repository;
using Vegas.Database.Mongo.Entity;

namespace Vegas.Database.Mongo.Repository
{
    public interface IMongoAsyncRepository<TEntity> : IAsyncRepository<TEntity, string>
        where TEntity : MongoEntity
    { }
}
