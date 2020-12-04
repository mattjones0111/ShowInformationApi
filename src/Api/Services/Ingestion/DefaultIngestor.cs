using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Api.Ports;
using Microsoft.Extensions.Logging;

namespace Api.Services.Ingestion
{
    /// <summary>
    ///    The default implementation of the ingestor.
    /// </summary>
    public class DefaultIngestor : IIngestor
    {
        private readonly IShowInformationSource _source;
        private readonly IShowInformationRepository _repository;
        private readonly ILogger<DefaultIngestor> _logger;

        public DefaultIngestor(
            IShowInformationSource source,
            IShowInformationRepository repository,
            ILogger<DefaultIngestor> logger)
        {
            _source = source;
            _repository = repository;
            _logger = logger;
        }

        public async Task GoAsync(CancellationToken cancellationToken)
        {
            IEnumerable<SourceShow> sourceShows;

            try
            {
                sourceShows = await _source.GetShowsAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieveing source information.");
                return;
            }

            foreach (SourceShow show in sourceShows)
            {
                try
                {
                    Show mapped = Map(show);
                    await _repository.PutAsync(mapped);
                }
                catch (Exception ex)
                {
                    // we're going to eat any exception that occurs while mapping/
                    // storing individual items; a failure of loading one item should
                    // not cause the rest of the operation to fail.
                    _logger.LogError(
                        ex,
                        "Error while mapping and storing source show.");
                }
            }
        }

        /// <summary>
        ///    Maps from <see cref="SourceShow"/> to <see cref="Show"/>.
        /// </summary>
        /// <remarks>
        ///    Potential optimisation: AutoMapper.
        /// </remarks>
        static Show Map(SourceShow source)
        {
            return new Show
            {
                Id = source.Id,
                Name = source.Name,
                Cast = source.Cast
                    .Select(x => new CastMember
                    {
                        Birthday = x.Birthday,
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToArray()
            };
        }
    }
}
