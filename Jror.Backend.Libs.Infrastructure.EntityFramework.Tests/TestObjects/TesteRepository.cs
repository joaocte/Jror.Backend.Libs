using Jror.Backend.Libs.Infrastructure.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.Tests.TestObjects
{
    public class TesteRepository<TContext> : Repository<TestClass, TContext> where TContext : DbContext
    {
        public TesteRepository(TContext context) : base(context)
        {
        }
    }
}