using System;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit transaction to save.
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();
    }
}