using NSubstitute;
using System.Data.Entity;
using Xunit;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void Add_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testObject = new TestClass();

            var context = Substitute.For<DbContext>();
            var dbSetMock = Substitute.For<DbSet<TestClass>>();

            context.Set<TestClass>().Returns(dbSetMock);
            dbSetMock.Add(Arg.Any<TestClass>()).Returns(testObject);

            // Act
            var repository = new TesteRepository<DbContext>(context);
            repository.AddAsync(testObject).Wait();

            //Assert
            context.Received().Set<TestClass>();
            dbSetMock.Received().Add(Arg.Any<TestClass>());
        }
    }
}