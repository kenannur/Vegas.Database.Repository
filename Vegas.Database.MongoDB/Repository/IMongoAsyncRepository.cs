using Vegas.Database.Abstraction.Repository;
using Vegas.Database.MongoDB.Entity;

namespace Vegas.Database.MongoDB.Repository
{
    public interface IMongoAsyncRepository<TEntity> : IAsyncRepository<TEntity, string>
        where TEntity : MongoEntity
    { }
}
