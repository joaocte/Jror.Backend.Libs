using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using DbContext = Microsoft.EntityFrameworkCore;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.Repository
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext.DbContext
    {
        private readonly TContext context;

        protected readonly DbSet<TEntity> DbSet;
        private bool disposedValue;

        public Repository(TContext context)
        {
            this.context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => DbSet.Add(obj), cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(condition, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DbSet.Where(condition).Any(), cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            var taskAttach = Task.Run(() => DbSet.Attach(obj), cancellationToken);
            var taskContext = Task.Run(() => context.Entry(obj).State = EntityState.Modified, cancellationToken);

            await Task.WhenAll(taskAttach, taskContext);
        }

        public async Task RemoveAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(id, cancellationToken);
            if (entity == null)
            {
                return;
            }
            if (context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(id, cancellationToken);

            return entity != null;
        }

        public async Task<IQueryable<TEntity>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DbSet, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}