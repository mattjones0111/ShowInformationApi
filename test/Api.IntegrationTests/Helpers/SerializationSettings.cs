using System.Text.Json;

namespace Api.IntegrationTests.Helpers
{
    internal static class SerializationSettings
    {
        public static JsonSerializerOptions Default =>
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
    }
}
