using Jror.Backend.Libs.Infrastructure.EntityFramework.Repository;
using System.Data.Entity;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.Tests
{
    public class TesteRepository<TContext> : Repository<TestClass, TContext> where TContext : DbContext
    {
        public TesteRepository(TContext context) : base(context)
        {
        }
    }
}