using System;
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

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            await Context.DeleteAsync<TEntity>(id, ct);
        }

        public async Task DeleteAsync(string hashKey, string rangeKey, CancellationToken ct = default)
        {
            await Context.DeleteAsync<TEntity>(hashKey, rangeKey, ct);
        }

        public async Task DeleteManyAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id, ct);
            }
        }

        public async Task<TEntity> GetAsync(Guid id, CancellationToken ct = default)
        {
            return await Context.LoadAsync<TEntity>(id, ct);
        }

        public async Task<TEntity> GetAsync(string hashKey, string rangeKey, CancellationToken ct = default)
        {
            return await Context.LoadAsync<TEntity>(hashKey, rangeKey, ct);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            await Context.SaveAsync(entity, ct);
            return entity;
        }
    }
}
