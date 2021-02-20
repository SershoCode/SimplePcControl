using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePcControl.Application.CommandHandlers;
using SimplePcControl.Host.Middlewares;
using System.IO;

namespace SimplePcControl.Host
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup(IHostEnvironment env) 
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings{(env.EnvironmentName == "Development" ? ".Development" : string.Empty)}.json", true, true);

            AppConfiguration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(MonitorsOffCommandHandler).Assembly);
            services.AddControllers();
            services.AddSwaggerGen();

            // Configure Options.
            services.Configure<MiddlewareOptions>(AppConfiguration.GetSection(nameof(MiddlewareOptions)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            // Используем Middleware для проверки авторизационного token'a к любом запросу в нашем api.
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
                {
                    appBuilder.UseMiddleware<CheckHeaderTokenMiddleware>();
                }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Debug section.
            if (!env.IsDevelopment()) return;

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
        }
    }
}
