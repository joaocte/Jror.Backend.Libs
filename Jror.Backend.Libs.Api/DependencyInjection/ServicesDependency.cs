using Jror.Backend.Libs.Api.Swagger;
using Jror.Backend.Libs.API.Abstractions;
using Jror.Backend.Libs.API.Abstractions.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Jror.Backend.Libs.Api.DependencyInjection
{
    public static class ServicesDependency
    {
        private static IJrorApiOption _jrApiOption;

        public static void AddServiceDependencyJrorApiSwagger(this IServiceCollection services, IConfiguration configuration, Func<IJrorApiOption> options = null)
        {
            _jrApiOption = options() ?? new JrorApiOption();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"V{_jrApiOption.MajorVersion}.{_jrApiOption.MinorVersion}", new OpenApiInfo
                {
                    Contact = new OpenApiContact
                    {
                        Email = _jrApiOption.Email,
                        Url = new Uri(_jrApiOption.Uri)
                    },
                    Title = _jrApiOption.Title,
                    Description = _jrApiOption.Description,
                });
            });

            services.AddScoped<IJrorApiOption>(x => _jrApiOption);
            services.AddApiVersioning(o =>
            {
                o.UseApiBehavior = false;
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(_jrApiOption.MajorVersion, _jrApiOption.MinorVersion);

                o.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader(_jrApiOption.HeaderApiVersionReader),
                    new UrlSegmentApiVersionReader());
            });
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = _jrApiOption.GroupNameFormat;
                o.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>>(provider => new ConfigureSwaggerOptions(provider.GetService<IApiVersionDescriptionProvider>(), _jrApiOption));
            services.Configure<IConfigureOptions<SwaggerGenOptions>>(configuration);
        }
    }
}