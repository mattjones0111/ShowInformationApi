using System.Threading;
using System.Threading.Tasks;

namespace Api.Services.Ingestion
{
    public interface IIngestor
    {
        Task GoAsync(CancellationToken cancellationToken);
    }
}
