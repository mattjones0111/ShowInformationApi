using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Services.Ingestion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Services
{
    /// <summary>
    ///    A hosted service which will trigger ingestion of source data.
    /// </summary>
    /// <remarks>
    ///    This service is here in order to run ingestion inside the Api
    ///    project. This is not ideal; this should be extracted to some other
    ///    executable/deployable, like a function or a worker service.
    /// </remarks>
    public class IngestionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public IngestionService(
            IServiceProvider serviceProvider,
            ILogger<IngestionService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Starting {GetType().Name}...");

            // hosted services are the first thing to be started, even before the
            // web application is running. We need a delay here, plus we need
            // an async operation to yield execution back, otherwise
            // the web host won't start until this is done.
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            _logger.LogInformation("Starting ingestion...");

            IIngestor ingestor = _serviceProvider.GetRequiredService<IIngestor>();

            await ingestor.GoAsync(stoppingToken);

            _logger.LogInformation("Finished ingestion.");
        }
    }
}
