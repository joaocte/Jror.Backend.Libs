using Jror.Backend.Libs.API.Abstractions.Interface;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace Jror.Backend.Libs.Api.Swagger
{
    /// <inheritdoc/>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly IJrorApiOption option;

        /// <inheritdoc/>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IJrorApiOption option)
        {
            this.provider = provider;
            this.option = option;
        }

        /// <inheritdoc/>
        public  void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            options.OperationFilter<SwaggerVersioningOperationFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ApiVersioning.xml"));
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = option.Title,
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
                info.Description += "[deprecated]";

            return info;
        }
    }
}