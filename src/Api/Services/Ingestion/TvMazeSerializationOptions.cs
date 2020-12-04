using System.Text.Json;

namespace Api.Services.Ingestion
{
    internal static class TvMazeSerializationOptions
    {
        public static JsonSerializerOptions Default = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
