using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Ports;

namespace Api.Adapters
{
    /// <summary>
    ///    An in-memory implementation of a simple document store.
    /// </summary>
    public sealed class InMemoryDocumentStore : IStoreDocuments
    {
        private readonly Dictionary<Type, Dictionary<int, object>> _state
            = new Dictionary<Type, Dictionary<int, object>>();

        public Task SaveAsync<T>(int id, T document)
        {
            if(!_state.ContainsKey(typeof(T)))
            {
                _state[typeof(T)] = new Dictionary<int, object>();
            }

            _state[typeof(T)][id] = document;

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAsync<T>(
            int pageNumber = 1,
            int pageSize = 10)
        {
            IEnumerable<T> result = Enumerable.Empty<T>();

            if (!_state.ContainsKey(typeof(T)))
            {
                return Task.FromResult(result);
            }

            result = _state[typeof(T)].Values
                .Skip(pageNumber - 1 * pageSize)
                .Take(pageSize)
                .Cast<T>();

            return Task.FromResult(result);
        }
    }
}
