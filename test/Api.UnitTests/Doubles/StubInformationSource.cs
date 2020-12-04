using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Services.Ingestion;

namespace Api.UnitTests.Doubles
{
    public sealed class StubInformationSource : IShowInformationSource
    {
        public Task<IEnumerable<SourceShow>> GetShowsAsync(CancellationToken cancellationToken)
        {
            IEnumerable<SourceShow> result = new [] {
                new SourceShow
                {
                    Id = 1,
                    Name = "The Jim Smith Show",
                    Cast = new []
                    {
                        new SourcePerson
                        {
                            Id = 1,
                            Name = "Jim Smith",
                            Birthday = DateTime.Now
                        }
                    }
                }
            };

            return Task.FromResult(result);
        }
    }
}
