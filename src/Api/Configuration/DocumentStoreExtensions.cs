using Api.Adapters;
using Api.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class DocumentStoreExtensions
    {
        public static IServiceCollection AddInMemoryDocumentStore(
            this IServiceCollection services)
        {
            services.AddSingleton<IStoreDocuments, InMemoryDocumentStore>();

            return services;
        }
    }
}
