using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Vegas.Database.Abstraction.Repository;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.DynamoDB.Repository
{
    public interface IDynamoAsyncRepository<TEntity> : IAsyncRepository<TEntity, string>
        where TEntity : DynamoEntity
    {
        Task CreateTableAsync(CreateTableRequest request, CancellationToken ct = default);

        Task UpdateTableAsync(UpdateTableRequest request, CancellationToken ct = default);
    }
}
