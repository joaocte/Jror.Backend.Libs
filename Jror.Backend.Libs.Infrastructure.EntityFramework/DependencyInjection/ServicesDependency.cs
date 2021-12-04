using Jror.Backend.Libs.Framework.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Jror.Backend.Libs.Infrastructure.EntityFramework.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrorInfrastructureEntityFramework(this IServiceCollection services)
        {
            services.AddServiceDependencyJrorFrameworkExceptionFilter();
        }
    }
}