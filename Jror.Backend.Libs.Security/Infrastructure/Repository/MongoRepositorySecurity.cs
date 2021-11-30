using Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Security.Infrastructure.Repository
{
    public abstract class MongoRepositorySecurity<TEntity> : IRepositorySecurity<TEntity> where TEntity : class
    {
        protected readonly IMongoContextSecurity Context;
        protected IMongoCollection<TEntity> DbSet;

        /// <inheritdoc/>
        protected MongoRepositorySecurity(IMongoContextSecurity context, string collectionName)
        {
            this.Context = context;

            DbSet = this.Context.GetCollection<TEntity>(collectionName);
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            var data = await DbSet
                .FindAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()), null, cancellationToken)
                .ConfigureAwait(false);

            return await data.SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DbSet.AsQueryable().Where(condition).FirstOrDefault(), cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DbSet.AsQueryable().Any(condition), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await DbSet.
                FindAsync(Builders<TEntity>.Filter.Empty, null, cancellationToken)
                .ConfigureAwait(false);
            return await all.ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }

            DbSet = null;
        }

        /// <inheritdoc/>
        public void Dispose() => Dispose(true);

        public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            var data = await DbSet
                .FindAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()), null, cancellationToken)
                .ConfigureAwait(false);

            return await data.AnyAsync(cancellationToken);
        }
    }
}