using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Vegas.Database.Abstraction.Repository;
using Vegas.Database.MongoDB.Entity;

namespace Vegas.Database.MongoDB.Repository
{
    public interface IMongoAsyncRepository<TEntity> : IAsyncRepository<TEntity, ObjectId>
        where TEntity : class, IMongoEntity
    {
        Task<TEntity> UpdateAsync(ObjectId id, UpdateDefinition<TEntity> update, CancellationToken ct = default);

        Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
    }
}
