using MongoDB.Driver;
using NSubstitute;

namespace Jror.Backend.Libs.Infrastructure.MongoDB.Tests.TestObjects
{
    public abstract class InfrastructureTestBase
    {
        protected IMongoCollection<T> CreateMockCollection<T>()
        {
            var settings = new MongoCollectionSettings();
            var mockCollection = Substitute.For<IMongoCollection<T>>();
            mockCollection.DocumentSerializer.Returns(settings.SerializerRegistry.GetSerializer<T>());
            mockCollection.Settings.Returns(settings);
            return mockCollection;
        }
    }
}