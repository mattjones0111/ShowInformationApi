using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Data.Model;
using Api.Ports;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Api.Adapters
{
    /// <summary>
    ///    A simple SQL-server implementation of a document store.
    /// </summary>
    public sealed class SqlDocumentStore : IStoreDocuments
    {
        private readonly ILogger _logger;
        private readonly SqlDocumentStoreOptions _options;

        public SqlDocumentStore(
            ILogger<SqlDocumentStore> logger,
            SqlDocumentStoreOptions options)
        {
            _logger = logger;
            _options = options;
        }

        public async Task SaveAsync<T>(int id, T document)
        {
            string json = JsonSerializer.Serialize(document);

            try
            {
                await using SqlConnection connection = new SqlConnection(_options.ConnectionString);

                connection.Open();

                Document toSave = new Document
                {
                    Id = id,
                    State = json,
                    TypeName = typeof(T).Name
                };

                await connection.ExecuteAsync(
                    "MERGE INTO Document AS target " +
                    "USING (SELECT @Id as Id, @TypeName as TypeName, @State as State) AS source " +
                    "ON source.Id = target.Id " +
                    "WHEN MATCHED THEN UPDATE SET State = @State " +
                    "WHEN NOT MATCHED THEN INSERT (Id, TypeName, State) " +
                    "VALUES (source.Id, source.TypeName, source.State);",
                    toSave);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Could not save document.");
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAsync<T>(
            int pageNumber = 1,
            int pageSize = 10)
        {
            string sql = "SELECT Id, TypeName, State " +
                         "FROM Document " +
                         "WHERE TypeName=@TypeName " +
                         "ORDER BY Id " +
                         "OFFSET @Offset ROWS " +
                         "FETCH NEXT @PageSize ROWS ONLY;";

            var parameters = new
            {
                TypeName = typeof(T).Name,
                Offset = (pageNumber - 1) * pageSize,
                PageSize = pageSize
            };

            IEnumerable<Document> documents;

            using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                documents = await connection.QueryAsync<Document>(sql, parameters);
            }

            return documents
                .Select(x => JsonSerializer.Deserialize<T>(x.State))
                .ToArray();
        }
    }

    public class SqlDocumentStoreOptions
    {
        public string ConnectionString { get; set; }
    }
}
