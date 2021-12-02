using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;
using Jror.Backend.Libs.Infrastructure.EntityFramework.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrorInfrastructureEntityFramework(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}