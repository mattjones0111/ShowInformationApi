using System;
using Api.Adapters;
using Api.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class DocumentStoreExtensions
    {
        // ReSharper disable once UnusedMember.Global - retained for convenience
        public static IServiceCollection AddInMemoryDocumentStore(
            this IServiceCollection services)
        {
            services.AddSingleton<IStoreDocuments, InMemoryDocumentStore>();

            return services;
        }

        public static IServiceCollection AddSqlDocumentStore(
            this IServiceCollection services,
            string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(
                    $"'{nameof(connectionString)}' cannot be null or empty.");
            }

            services.AddSingleton(provider => new SqlDocumentStoreOptions
            {
                ConnectionString = connectionString
            });

            services.AddSingleton<IStoreDocuments, SqlDocumentStore>();

            return services;
        }
    }
}
