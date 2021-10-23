using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.Abstraction.Repository
{
    public interface IAsyncRepository<TEntity, TId>
        where TEntity : IEntity<TId>
        where TId : struct
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);

        Task<TEntity> GetAsync(TId id, CancellationToken ct = default);

        Task DeleteAsync(TId id, CancellationToken ct = default);

        Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default);

        Task DeleteManyAsync(IEnumerable<TId> ids, CancellationToken ct = default);
    }
}
