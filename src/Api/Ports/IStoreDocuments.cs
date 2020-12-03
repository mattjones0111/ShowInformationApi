using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Ports
{
    public interface IStoreDocuments
    {
        Task SaveAsync<T>(int id, T document);

        Task<IEnumerable<T>> GetAsync<T>(int pageNumber = 1, int pageSize = 10);
    }
}
