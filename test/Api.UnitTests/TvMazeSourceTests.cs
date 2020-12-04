using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Api.Services.Ingestion;
using Api.UnitTests.Doubles;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace Api.UnitTests
{
    public class TvMazeSourceTests
    {
        [Test]
        public async Task CanRetrieveShowsFromTvMaze()
        {
            // arrange
            IHttpClientFactory clientFactory = new StubHttpClientFactory();

            TvMazeInformationSource subject = new TvMazeInformationSource(
                clientFactory,
                new NullLogger<TvMazeInformationSource>());

            // act
            IEnumerable<SourceShow> actual =
                await subject.GetShowsAsync(CancellationToken.None);

            // assert
            Assert.IsNotNull(actual);

            SourceShow underTheDome = actual.Single();

            Assert.AreEqual("Under the Dome", underTheDome.Name);
            Assert.AreEqual(1, underTheDome.Id);

            string[] castMemberNames = { 
                "Mike Vogel",
                "Rachelle Lefevre",
                "Alexander Koch",
                "Colin Ford",
                "Mackenzie Lintz",
                "Dean Norris",
                "Eddie Cahill",
                "Nicholas Strong",
                "Britt Robertson",
                "Aisha Hinds",
                "Natalie Martinez",
                "Karla Crome",
                "Kylie Bunbury",
                "Jolene Purdy",
                "Jeff Fahey"
            };

            CollectionAssert.AreEquivalent(
                castMemberNames,
                underTheDome.Cast.Select(x => x.Name));
        }
    }
}
