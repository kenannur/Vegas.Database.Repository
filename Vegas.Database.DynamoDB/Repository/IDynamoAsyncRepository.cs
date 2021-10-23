using System;
using System.Threading;
using System.Threading.Tasks;
using Vegas.Database.Abstraction.Repository;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.DynamoDB.Repository
{
    public interface IDynamoAsyncRepository<TEntity> : IAsyncRepository<TEntity, Guid>
        where TEntity : class, IDynamoEntity
    {
        Task<TEntity> GetAsync(string hashKey, string rangeKey, CancellationToken ct = default);

        Task DeleteAsync(string hashKey, string rangeKey, CancellationToken ct = default);
    }
}
