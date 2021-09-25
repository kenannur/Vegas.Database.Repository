using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Vegas.Database.Abstraction.Repository;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.DynamoDB.Repository
{
    public interface IDynamoAsyncRepository<TEntity> : IAsyncRepository<TEntity, string>
        where TEntity : DynamoEntity
    {
        Task<List<string>> GetTablesAsync();

        Task CreateTablesAsync(Assembly assembly);

        Task DeleteTablesAsync();
    }
}
