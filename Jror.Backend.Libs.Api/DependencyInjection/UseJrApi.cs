using Jror.Backend.Libs.API.Abstractions;
using Jror.Backend.Libs.API.Abstractions.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Jror.Backend.Libs.Api.DependencyInjection
{
    public static class UseJrApi
    {
        public static void UseJrApiSwaggerSecurity(this IApplicationBuilder app, IWebHostEnvironment env, Func<IJrorApiOption> options = null)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            UseJrApiSwagger(app, env, options);
        }

        public static void UseJrApiSwagger(this IApplicationBuilder app, IWebHostEnvironment env, Func<IJrorApiOption> options = null)
        {
            var jrApiOption = options() ?? new JrorApiOption();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", jrApiOption.Title));
            }
        }
    }
}