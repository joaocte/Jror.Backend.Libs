using Jror.Backend.Libs.Framework.DependencyInjection;
using Jror.Backend.Libs.Infrastructure.Data.Shared.Interfaces;
using Jror.Backend.Libs.Infrastructure.EntityFramework.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrorInfrastructureEntityFramework<TContext>(this IServiceCollection services)
        where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork>(p =>
            {
                var dbContext = services.BuildServiceProvider().GetRequiredService<TContext>();

                return new UnitOfWork<TContext>(dbContext);
            });
            services.AddServiceDependencyJrorFrameworkExceptionFilter();
        }
    }
}