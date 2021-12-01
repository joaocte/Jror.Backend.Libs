using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;
using Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Infrastructure.MongoDB.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoContext _context;
        protected IMongoCollection<T> _dbSet;

        /// <inheritdoc/>
        protected Repository(IMongoContext context, string collectionName)
        {
            this._context = context;

            _dbSet = this._context.GetCollection<T>(collectionName);
        }

        /// <inheritdoc/>
        public virtual async Task AddAsync(T obj, CancellationToken cancellationToken = default)
        {
            await _context.AddCommand(() => _dbSet.InsertOneAsync(obj, null, cancellationToken)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            var data = await _dbSet
                .FindAsync(Builders<T>.Filter.Eq("_id", id.ToString()), null, cancellationToken)
                .ConfigureAwait(false);

            return await data.SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _dbSet.AsQueryable().Where(condition).FirstNonDefault(), cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _dbSet.AsQueryable().Any(condition), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _dbSet.
                FindAsync(Builders<T>.Filter.Empty, null, cancellationToken)
                .ConfigureAwait(false);
            return await all.ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(T obj, CancellationToken cancellationToken = default)
        {
            await _context.AddCommand(() => _dbSet.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", obj.GetId()), obj, cancellationToken: cancellationToken)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task RemoveAsync(object id, CancellationToken cancellationToken = default)
        {
            await _context.AddCommand(() => _dbSet.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id.ToString()), cancellationToken)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _dbSet = null;
        }

        /// <inheritdoc/>
        public void Dispose() => Dispose(true);

        public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            var data = await _dbSet
                .FindAsync(Builders<T>.Filter.Eq("_id", id.ToString()), null, cancellationToken)
                .ConfigureAwait(false);

            return await data.AnyAsync(cancellationToken);
        }

        public async Task<IQueryable<T>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _dbSet.AsQueryable(), cancellationToken);
        }
    }
}