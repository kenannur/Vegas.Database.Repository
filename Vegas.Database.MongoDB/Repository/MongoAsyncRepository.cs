using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Vegas.Database.MongoDB.Context;
using Vegas.Database.MongoDB.Entity;

namespace Vegas.Database.MongoDB.Repository
{
    public class MongoAsyncRepository<TEntity> : IMongoAsyncRepository<TEntity>
        where TEntity : MongoEntity
    {
        protected readonly MongoDbContext Context;
        public MongoAsyncRepository(MongoDbContext dbContext) => Context = dbContext;

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            ThrowIfNull(entity);
            await Context.Collection<TEntity>().InsertOneAsync(entity, default, ct);
            return entity;
        }

        public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            ThrowIfNull(entities);
            await Context.Collection<TEntity>().InsertManyAsync(entities, default, ct);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            ThrowIfNull(entity);
            await Context.Collection<TEntity>().DeleteOneAsync(x => x.Id == entity.Id, ct);
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            ThrowIfNull(entities);
            await Context.Collection<TEntity>().DeleteManyAsync(x => entities.Select(x => x.Id).Contains(x.Id), default, ct);
        }

        public virtual async Task<TEntity> GetAsync(string id, CancellationToken ct = default)
        {
            ThrowIfNull(id);
            return await Context.Collection<TEntity>().Find(x => x.Id == id).FirstOrDefaultAsync(ct);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            ThrowIfNull(entity);
            await Context.Collection<TEntity>().ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions(), ct);
            return entity;
        }

        private static void ThrowIfNull(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(obj.GetType().Name);
            }
        }
    }
}
