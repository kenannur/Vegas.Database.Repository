using Vegas.Database.Abstraction.Repository;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.DynamoDB.Repository
{
    public interface IDynamoAsyncRepository<TEntity> : IAsyncRepository<TEntity, string>
        where TEntity : DynamoEntity
    {

    }
}
