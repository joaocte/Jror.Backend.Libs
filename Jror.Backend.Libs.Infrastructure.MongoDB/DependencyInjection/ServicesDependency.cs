using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;
using Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions;
using Jror.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Jror.Backend.Libs.Infrastructure.MongoDB.Context;
using Jror.Backend.Libs.Infrastructure.MongoDB.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Jror.Backend.Libs.Infrastructure.MongoDB.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrInfrastructureMongoDb(this IServiceCollection services, ConnectionType connectionType = ConnectionType.ReplicaSet)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLowerInvariant();
            services.AddScoped<IMongoContext>((_) =>
            {
                var config = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                                 .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                                 .AddEnvironmentVariables()
                                 .Build();

                return new MongoContext(config, connectionType);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}