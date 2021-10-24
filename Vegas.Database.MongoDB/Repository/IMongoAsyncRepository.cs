using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Vegas.Database.Abstraction.Repository;
using Vegas.Database.MongoDB.Entity;

namespace Vegas.Database.MongoDB.Repository
{
    public interface IMongoAsyncRepository<TEntity> : IAsyncRepository<TEntity, string>
        where TEntity : MongoEntity
    {
        Task<TEntity> UpdateAsync(string id, UpdateDefinition<TEntity> update, CancellationToken ct = default);

        Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
    }
}
