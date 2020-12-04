using System;
using System.Net.Http;

namespace Api.UnitTests.Doubles
{
    public sealed class StubHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient(new StubTvMazeHandler())
            {
                BaseAddress = new Uri("http://tvmaze.local")
            };
        }
    }
}