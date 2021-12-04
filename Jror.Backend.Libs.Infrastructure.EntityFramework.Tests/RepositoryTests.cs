using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;
using Jror.Backend.Libs.Infrastructure.EntityFramework.Tests.TestObjects;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.Tests
{
    public class RepositoryTests
    {
        private readonly TestClassContext context;
        private DbSet<TestClass> dbSetMock;
        private IRepository<TestClass> repository;
        private readonly TestClass testObject;

        public RepositoryTests()
        {
            dbSetMock = Substitute.For<DbSet<TestClass>>();

            context = Substitute.For<TestClassContext>(dbSetMock);
            testObject = new TestClass();
        }

        [Fact]
        public void AddAsync_TestClassObjectPassed_ProperMethodCalled()
        {
            context.Set<TestClass>().Returns(dbSetMock);
            repository = new TesteRepository<TestClassContext>(context);

            dbSetMock.Add(Arg.Any<TestClass>());

            // Act
            repository.AddAsync(testObject).Wait();

            //Assert
            context.Received().Set<TestClass>();
            dbSetMock.Received().Add(Arg.Any<TestClass>());
        }

        //[Fact]
        //public void RemoveAsync_TestClassObjectPassed_ProperMethodCalled()
        //{
        //    // Arrange
        //    context.Set<TestClass>().Returns(dbSetMock);
        //    repository = new TesteRepository<TestClassContext>(context);
        //    context.SetModified(testObject);
        //    dbSetMock.FindAsync(Arg.Any<object>()).Returns(testObject);
        //    dbSetMock.Remove(Arg.Any<TestClass>()).Returns(testObject);
        //    // Act
        //    repository.RemoveAsync(testObject);

        //    //Assert
        //    context.Received().Set<TestClass>();
        //    dbSetMock.Received().Remove(Arg.Any<TestClass>());
        //}

        [Fact]
        public void GetByIdAsync_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            context.Set<TestClass>().Returns(dbSetMock);
            repository = new TesteRepository<TestClassContext>(context);

            dbSetMock.FindAsync(Arg.Any<object>()).Returns(testObject);

            // Act
            repository.GetByIdAsync(1);

            // Assert
            context.Received().Set<TestClass>();
            dbSetMock.Received().FindAsync(1);
        }

        [Fact]
        public void GetAsync_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            context.Set<TestClass>().Returns(dbSetMock);
            repository = new TesteRepository<TestClassContext>(context);

            dbSetMock.FindAsync(Arg.Any<object>()).Returns(testObject);

            // Act
            repository.GetAsync(x => x.Id == 1);

            // Assert
            context.Received().Set<TestClass>();
            dbSetMock.Received().FindAsync(Arg.Any<object>());
        }

        [Fact]
        public void GetAllAsQueryableAsync_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            context.Set<TestClass>().Returns(dbSetMock);
            repository = new TesteRepository<TestClassContext>(context);

            // Act
            repository.GetAllAsQueryableAsync();

            // Assert
            context.Received().Set<TestClass>();
        }

        [Fact]
        public void ExistsAsync_TestClassObjectPassed_ProperMethodCalled()
        {
            context.Set<TestClass>().Returns(dbSetMock);
            repository = new TesteRepository<TestClassContext>(context);

            dbSetMock.FindAsync(Arg.Any<object>()).Returns(testObject);

            // Act
            var retorno = repository.ExistsAsync(testObject.Id).Result;

            //Assert
            context.Received().Set<TestClass>();
            dbSetMock.Received().FindAsync(Arg.Any<object>());

            Assert.True(retorno);
        }

        //[Fact]
        //public void UpdateAsync_TestClassObjectPassed_ProperMethodCalled()
        //{
        //    context.Set<TestClass>().Returns(dbSetMock);
        //    repository = new TesteRepository<TestClassContext>(context);
        //    // Act
        //    repository.UpdateAsync(testObject).Wait();

        //    //Assert
        //    context.Received().Set<TestClass>();
        //    dbSetMock.Received().Attach(Arg.Any<TestClass>());
        //}
    }
}