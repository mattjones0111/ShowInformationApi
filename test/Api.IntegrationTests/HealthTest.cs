using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Api.IntegrationTests
{
    public class HealthTest
    {
        private ApiWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _factory = new ApiWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task HealthTestReturnsOk()
        {
            HttpResponseMessage actual = await _client.GetAsync("health");

            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
