using Microsoft.AspNetCore.Builder;

namespace Jror.Backend.Libs.Security.Middleware
{
    public static class SecurityApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSecurity(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityMiddleware>();
        }
    }
}