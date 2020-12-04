using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Api.Services.Ingestion
{
    /// <summary>
    ///    An adapter that will retrieve show information from TvMaze.
    /// </summary>
    public sealed class TvMazeInformationSource : IShowInformationSource
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public TvMazeInformationSource(
            IHttpClientFactory httpClientFactory,
            ILogger<TvMazeInformationSource> logger)
        {
            _client = httpClientFactory.CreateClient("tv-maze");
            _logger = logger;
        }

        public async Task<IEnumerable<SourceShow>> GetShowsAsync(
            CancellationToken cancellationToken)
        {
            SourceShow[] shows = await GetAsync<SourceShow[]>(
                "shows",
                cancellationToken);

            Task[] tasks = shows
                .Select(x => AppendCast(x, cancellationToken))
                .ToArray();

            await Task.WhenAll(tasks);

            return shows;
        }

        private async Task AppendCast(
            SourceShow show,
            CancellationToken cancellationToken)
        {
            IEnumerable<SourceCastDocument> cast =
                await GetAsync<IEnumerable<SourceCastDocument>>(
                    $"shows/{show.Id}/cast",
                    cancellationToken);

            show.Cast = cast.Select(x => x.Person).ToArray();
        }

        private async Task<T> GetAsync<T>(
            string url,
            CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response =
                    await _client.GetAsync(url, cancellationToken);

                string json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(
                    json,
                    TvMazeSerializationOptions.Default);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling TvMaze api, url={url}", url);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing response from TvMaze, url={url}", url);
                throw;
            }
        }
    }
}