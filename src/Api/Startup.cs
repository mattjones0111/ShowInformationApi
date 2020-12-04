using System.Reflection;
using Api.Configuration;
using Api.Data;
using Api.Middleware.Health;
using Api.Services.Ingestion;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddFluentValidation(configure =>
                {
                    configure.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Show Inforamtion Api",
                        Version = "v1"
                    });
            });

            services.AddShowInformationProvider();
            services.AddSqlDocumentStore(Configuration.GetConnectionString("database"));
            services.AddTvMazeIngestor();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseApiHealthChecks();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UpgradeDatabase(Configuration["ConnectionStrings:Database"]);
        }
    }
}
