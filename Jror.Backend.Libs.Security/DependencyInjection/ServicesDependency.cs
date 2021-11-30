using Jror.Backend.Libs.Security.Abstractions;
using Jror.Backend.Libs.Security.Abstractions.Application;
using Jror.Backend.Libs.Security.Abstractions.Entity;
using Jror.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces;
using Jror.Backend.Libs.Security.Application;
using Jror.Backend.Libs.Security.Context;
using Jror.Backend.Libs.Security.Infrastructure.Repository.MongoDb;
using Jror.Backend.Libs.Framework.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Jror.Backend.Libs.Security.DependencyInjection
{
    public static class ServicesDependency
    {
        public static void AddServiceDependencyJrSecurityApi(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = Constants.TokenValidationParameters;
                });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer {token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void AddServiceDependencyJrSecurityApiUsingCustomValidate(this IServiceCollection services, Func<ISecurityConfiguration> configuration)
        {
            services.AddScoped<IMongoContextSecurity>((_) =>
            {
                var securityConfiguration = configuration();
                if (securityConfiguration == null)
                    throw new ArgumentNullException(nameof(configuration));

                var config =
                    new ConfigurationBuilder()
                    .AddInMemoryCollection(securityConfiguration.InMemoryCollection)
                    .Build();

                return new MongoContextSecurity(config);
            });

            services.AddScoped<ITenantRepositorySecurity>(p =>
            {
                var mongoContext = p.GetService<IMongoContextSecurity>();
                return new TenantRepositorySecurity(mongoContext, nameof(Tenant));
            });

            services.AddServiceDependencyJrorFrameworkExceptionFilter();

            AddServiceDependencyJrSecurityApi(services);
            services.AddScoped<IValidateToken, ValidateToken>();
        }
    }
}