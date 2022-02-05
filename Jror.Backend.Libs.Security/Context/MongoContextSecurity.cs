using Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Jror.Backend.Libs.Security.Context
{
    public class MongoContextSecurity : IMongoContextSecurity
    {
        private IMongoDatabase Database { get; set; }
        public MongoClient MongoClient { get; set; }

        private readonly IConfiguration configuration;

        /// <inheritdoc/>
        public MongoContextSecurity(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(configuration["Connection"]);

            Database = MongoClient.GetDatabase(configuration["DatabaseName"]);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
        }
    }
}