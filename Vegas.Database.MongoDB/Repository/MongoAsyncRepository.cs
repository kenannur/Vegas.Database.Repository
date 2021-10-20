﻿using System;
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

        public async Task DeleteAsync(string id, CancellationToken ct = default)
        {
            await Context.Collection<TEntity>().DeleteOneAsync(x => x.Id == id, ct);
        }

        public async Task DeleteManyAsync(IEnumerable<string> ids, CancellationToken ct = default)
        {
            await Context.Collection<TEntity>().DeleteManyAsync(x => ids.Contains(x.Id), default, ct);
        }

        public virtual async Task<TEntity> GetAsync(string id, CancellationToken ct = default)
        {
            ThrowIfNull(id);
            return await Context.Collection<TEntity>().Find(x => x.Id == id).FirstOrDefaultAsync(ct);
        }

        /// <summary>
        /// Replaces the entity according to the entity id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            ThrowIfNull(entity);
            await Context.Collection<TEntity>().ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions(), ct);
            return entity;
        }

        /// <summary>
        /// Updates specified fields atomically according to the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(string id, UpdateDefinition<TEntity> update, CancellationToken ct = default)
        {
            ThrowIfNull(id);
            ThrowIfNull(update);
            var options = new FindOneAndUpdateOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After
            };
            return await Context.Collection<TEntity>()
                                .FindOneAndUpdateAsync<TEntity>(x => x.Id == id, update, options, ct);
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
