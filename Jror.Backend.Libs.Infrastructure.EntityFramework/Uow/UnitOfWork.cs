using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await context.SaveChangesAsync() > 0;
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