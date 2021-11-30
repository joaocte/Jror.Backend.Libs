using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces
{
    public interface IRepositorySecurity<T> : IDisposable where T : class
    {
        /// <summary>
        ///Returns the Object by Id if exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        ///Returns the Object by Id if exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        /// <summary>
        /// This method takes <see cref="Expression"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="T"/>.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        ///TODO
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default);
    }
}