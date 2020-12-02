using Api.Adapters;
using Api.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class ProviderExtensions
    {
        public static IServiceCollection AddShowInformationProvider(
            this IServiceCollection services)
        {
            services.AddTransient<IShowInformationRepository, InMemoryShowInformationRepository>();

            return services;
        }
    }
}
