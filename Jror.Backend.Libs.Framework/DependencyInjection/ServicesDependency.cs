using Jror.Backend.Libs.Domain.Abstractions.Notifications;
using Jror.Backend.Libs.Domain.Notifications;
using Jror.Backend.Libs.Framework.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Jror.Backend.Libs.Framework.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrorFramework(this IServiceCollection services)
        {
            services.AddServiceDependencyJrorFrameworkNotificationContext();
            services.AddServiceDependencyJrorFrameworkExceptionFilter();
        }

        public static void AddServiceDependencyJrorFrameworkNotificationContext(this IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();
            services.AddMvcCore(options =>
            {
                options.Filters.Add<NotificationFilter>();
            });
        }

        public static void AddServiceDependencyJrorFrameworkExceptionFilter(this IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });
        }
    }
}