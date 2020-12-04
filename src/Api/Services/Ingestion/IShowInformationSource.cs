using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services.Ingestion
{
    /// <summary>
    ///    An interface that defines the contract for a service that will
    ///    retrieve source Show information.
    /// </summary>
    public interface IShowInformationSource
    {
        Task<IEnumerable<SourceShow>> GetShowsAsync(CancellationToken cancellationToken);
    }

    public class SourceCastDocument
    {
        public SourcePerson Person { get; set; }
    }

    public class SourcePerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }

    public class SourceShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SourcePerson[] Cast { get; set; }
    }
}
