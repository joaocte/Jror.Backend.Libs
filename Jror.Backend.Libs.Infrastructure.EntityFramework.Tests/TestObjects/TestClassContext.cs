using Microsoft.EntityFrameworkCore;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.Tests.TestObjects
{
    public class TestClassContext : DbContext, ITestClass
    {
        public TestClassContext(DbSet<TestClass> dbSetTestClass)
        {
            DbSetTestClass = dbSetTestClass;
        }

        public DbSet<TestClass> DbSetTestClass { get; set; }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}