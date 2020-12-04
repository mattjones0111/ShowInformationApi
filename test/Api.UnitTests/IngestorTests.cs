using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Adapters;
using Api.Models;
using Api.Ports;
using Api.Services.Ingestion;
using Api.UnitTests.Doubles;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace Api.UnitTests
{
    public class IngestorTests
    {
        [Test]
        public async Task CanIngestData()
        {
            // arrange
            InMemoryDocumentStore inMemoryStore = new InMemoryDocumentStore();
            IShowInformationSource source = new StubInformationSource();
            IShowInformationRepository repository = new DefaultShowInformationRepository(inMemoryStore);

            DefaultIngestor subject = new DefaultIngestor(
                source,
                repository,
                new NullLogger<DefaultIngestor>());

            // act
            await subject.GoAsync(CancellationToken.None);

            // assert
            IEnumerable<Show> actual = await inMemoryStore.GetAsync<Show>();

            Show theShow = actual.SingleOrDefault();

            Assert.IsNotNull(theShow);
            Assert.AreEqual("The Jim Smith Show", theShow.Name);
        }
    }
}
