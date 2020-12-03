using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Ports;

namespace Api.Adapters
{
    /// <summary>
    ///    The default show information provider.
    /// </summary>
    public sealed class DefaultShowInformationRepository : IShowInformationRepository
    {
        private readonly IStoreDocuments _documentStore;

        public DefaultShowInformationRepository(IStoreDocuments documentStore)
        {
            _documentStore = documentStore;
        }

        public Task<IEnumerable<Show>> GetShowsAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            return _documentStore.GetAsync<Show>(pageNumber, pageSize);
        }

        public Task PutAsync(Show show)
        {
            show.Cast = show.Cast
                .OrderByDescending(x => x.Birthday)
                .ToArray();

            return _documentStore.SaveAsync(show.Id, show);
        }
    }
}
