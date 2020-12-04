using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.UnitTests.Helpers;

namespace Api.UnitTests.Doubles
{
    /// <summary>
    ///    A <see cref="DelegatingHandler"/> that mocks a tiny subset of the
    ///    TvMaze Api and data.
    /// </summary>
    public sealed class StubTvMazeHandler : DelegatingHandler
    {
        private readonly Dictionary<string, string> _responses
            = new Dictionary<string, string>
            {
                {"/shows", "Api.UnitTests.Data.tvmaze-api-shows.json"},
                {"/shows/1/cast", "Api.UnitTests.Data.tvmaze-api-show-1-cast.json"}
            };

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                new HttpResponseMessage(HttpStatusCode.NotFound);

            if (request.Method == HttpMethod.Get &&
                _responses.ContainsKey(request.RequestUri.AbsolutePath))
            {
                string json = EmbeddedResource.GetString(
                    _responses[request.RequestUri.AbsolutePath]);

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json")
                };
            }

            return Task.FromResult(response);
        }
    }
}
