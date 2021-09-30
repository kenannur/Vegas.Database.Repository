using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.DynamoDB.Repository
{
    public class DynamoAsyncRepository<TEntity> : IDynamoAsyncRepository<TEntity>
        where TEntity : class, IDynamoEntity
    {
        protected readonly IAmazonDynamoDB Client;
        protected readonly IDynamoDBContext Context;
        protected const string KeySeperator = "#"; 

        public DynamoAsyncRepository(IAmazonDynamoDB client, IDynamoDBContext context)
        {
            Client = client;
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await Context.SaveAsync(entity, ct);
            return entity;
        }

        public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity, ct);
            }
        }

        public async Task DeleteAsync(string id, CancellationToken ct = default)
        {
            if (id.Contains(KeySeperator))
            {
                var (hashKey, rangeKey) = SplitId(id);
                await Context.DeleteAsync<TEntity>(hashKey, rangeKey, ct);
            }
            await Context.DeleteAsync<TEntity>(id, ct);
        }

        public async Task DeleteManyAsync(IEnumerable<string> ids, CancellationToken ct = default)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id, ct);
            }
        }

        public async Task<TEntity> GetAsync(string id, CancellationToken ct = default)
        {
            if (id.Contains(KeySeperator))
            {
                var (hashKey, rangeKey) = SplitId(id);
                return await Context.LoadAsync<TEntity>(hashKey, rangeKey, ct);
            }
            return await Context.LoadAsync<TEntity>(id, ct);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            await Context.SaveAsync(entity, ct);
            return entity;
        }

        private (string hashKey, string rangeKey) SplitId(string id)
        {
            var splittedKeys = id.Split(KeySeperator);
            var hashKey = splittedKeys[0];
            var rangeKey = splittedKeys[1];
            return (hashKey, rangeKey);
        }
    }
}
