using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Api.Services.Ingestion
{
    public static class IngestorServiceExtensions
    {
        public static IServiceCollection AddTvMazeIngestor(
            this IServiceCollection services)
        {
            services.AddHostedService<IngestionService>();
            services.AddTransient<IShowInformationSource, TvMazeInformationSource>();
            services.AddTransient<IIngestor, DefaultIngestor>();
            services.AddHttpClient(
                "tv-maze",
                client =>
                {
                    client.BaseAddress = new Uri("http://api.tvmaze.com");
                })
                .AddPolicyHandler(RetryPolicy);

            return services;
        }

        private const int NumberOfRetries = 5;

        /// <summary>
        ///    Creates a retry policy which will retry the request if the response
        ///    is a transient error or we trip the rate limit. Retries are attempted
        ///    with an expotentially-increasing pause between them.
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> RetryPolicy
        {
            get
            {
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(result => result.StatusCode == HttpStatusCode.TooManyRequests)
                    .WaitAndRetryAsync(
                        NumberOfRetries,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            }
        }
    }
}
