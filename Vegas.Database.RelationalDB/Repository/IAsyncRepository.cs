using Vegas.Database.Abstraction.Repository;
using Vegas.Database.RelationalDB.Entity;

namespace Vegas.Database.RelationalDB.Repository
{
    public interface IAsyncRepository<TEntity> : IAsyncRepository<TEntity, long>
        where TEntity : RelationalEntity
    { }
}
