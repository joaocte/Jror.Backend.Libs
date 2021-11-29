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
            services.AddServiceDependencyJrFrameworkNotificationContext();
            services.AddServiceDependencyJrFrameworkExceptionFilter();
        }

        public static void AddServiceDependencyJrFrameworkNotificationContext(this IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();
            services.AddMvcCore(options =>
            {
                options.Filters.Add<NotificationFilter>();
            });
        }

        public static void AddServiceDependencyJrFrameworkExceptionFilter(this IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });
        }
    }
}