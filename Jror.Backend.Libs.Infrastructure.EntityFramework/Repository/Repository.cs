using Jror.Backend.Libs.Domain.Abstractions.Exceptions;
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

        public virtual async Task AddAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            DbSet.Add(obj);

            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(condition);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DbSet.Where(condition).Any());
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task UpdateAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            DbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RemoveAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Não foi encontrado o id: {id}");
            }
            if (context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual  async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(id);

            return entity != null;
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
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

        public virtual void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}