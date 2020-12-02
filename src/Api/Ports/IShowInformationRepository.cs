using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Ports
{
    public interface IShowInformationRepository
    {
        Task<IEnumerable<Show>> GetShowsAsync(int pageNumber = 1, int pageSize = 10);

        Task PutAsync(Show show);
    }
}
