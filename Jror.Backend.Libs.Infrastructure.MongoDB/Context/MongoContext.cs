using Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions;
using Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jror.Backend.Libs.Infrastructure.MongoDB.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> commands;

        private readonly ConnectionType conectionType;
        private readonly IConfiguration configuration;

        /// <inheritdoc/>
        public MongoContext(IConfiguration configuration, ConnectionType conectionType = ConnectionType.ReplicaSet)
        {
            this.configuration = configuration;

            commands = new List<Func<Task>>();
            this.conectionType = conectionType;
        }

        public virtual async Task<int> SaveChanges()
        {
            ConfigureMongo();

            return ConnectionType.ReplicaSet.Equals(this.conectionType)
                ? await SaveChangesReplicaSet()
                : await SaveChangesDirectConnection();
        }

        private async Task<int> SaveChangesReplicaSet()
        {
            using (Session = await MongoClient.StartSessionAsync().ConfigureAwait(false))
            {
                Session.StartTransaction();

                var commandTasks = commands.Select(c => c());

                await Task.WhenAll(commandTasks).ConfigureAwait(false);

                await Session.CommitTransactionAsync().ConfigureAwait(false);
            }

            return commands.Count;
        }

        private async Task<int> SaveChangesDirectConnection()
        {
            var commandTasks = commands.Select(c => c());

            await Task.WhenAll(commandTasks).ConfigureAwait(false);

            return commands.Count;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(configuration["MongoSettings:Connection"]);

            Database = MongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"]);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public async Task AddCommand(Func<Task> func) => await Task.Run(() => commands.Add(func)).ConfigureAwait(false);
    }
}