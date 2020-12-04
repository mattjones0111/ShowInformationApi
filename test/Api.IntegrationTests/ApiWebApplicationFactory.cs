using Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api.IntegrationTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(config =>
            {
                // we need to stop the ingestor from starting during these
                // integration tests.
                config.Remove(
                    new ServiceDescriptor(
                        typeof(IHostedService),
                        typeof(IngestionService)));
            });
        }
    }
}
