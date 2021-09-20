using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vegas.Database.RelationalDB.Entity;

namespace Vegas.Database.RelationalDB.Repository
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : RelationalEntity
    {
        protected readonly DbContext Context;
        public AsyncRepository(DbContext context) => Context = context;

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            ThrowIfNull(entity);
            Context.Add(entity);
            await Context.SaveChangesAsync(ct);
            return entity;
        }

        public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            ThrowIfNull(entities);
            Context.AddRange(entities);
            await Context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            ThrowIfNull(entity);
            Context.Remove(entity);
            await Context.SaveChangesAsync(ct);
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            ThrowIfNull(entities);
            Context.RemoveRange(entities);
            await Context.SaveChangesAsync(ct);
        }

        public async Task<TEntity> GetAsync(long id, CancellationToken ct = default)
        {
            ThrowIfNull(id);
            return await Context.FindAsync<TEntity>(new object[] { id }, ct);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            ThrowIfNull(entity);
            Context.Update(entity);
            await Context.SaveChangesAsync(ct);
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
