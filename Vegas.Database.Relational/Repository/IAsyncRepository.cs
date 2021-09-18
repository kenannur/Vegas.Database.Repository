using Vegas.Database.Abstraction.Entity;
using Vegas.Database.Abstraction.Repository;

namespace Vegas.Database.Relational.Repository
{
    public interface IAsyncRepository<TEntity> : IAsyncRepository<TEntity, long>
        where TEntity : class, IEntity<long>
    { }
}
