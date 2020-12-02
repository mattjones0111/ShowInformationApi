using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Ports;

namespace Api.Adapters
{
    /// <summary>
    ///    A show information provider that uses in-memory storage.
    /// </summary>
    public sealed class InMemoryShowInformationRepository : IShowInformationRepository
    {
        private static readonly Dictionary<int, Show> Storage =
            new Dictionary<int, Show>();

        public Task<IEnumerable<Show>> GetShowsAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            IEnumerable<Show> result = Storage.Values
                .Skip(pageNumber - 1 * pageSize)
                .Take(pageSize);

            return Task.FromResult(result);
        }

        public Task PutAsync(Show show)
        {
            Storage[show.Id] = show;

            return Task.CompletedTask;
        }
    }
}
