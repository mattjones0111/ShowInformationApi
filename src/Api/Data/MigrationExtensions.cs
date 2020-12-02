using System;
using System.Reflection;
using DbUp;
using DbUp.Engine.Output;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Data
{
    public static class MigrationExtensions
    {
        public static IApplicationBuilder UpgradeDatabase(
            this IApplicationBuilder builder,
            string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(
                    $"{nameof(connectionString)} cannot be null or empty.");
            }

            ILoggerFactory loggerFactory = builder.ApplicationServices
                .GetRequiredService<ILoggerFactory>();

            ILogger<DbUpLoggerAdapter> logger =
                loggerFactory.CreateLogger<DbUpLoggerAdapter>();

            DbUpLoggerAdapter logAdapter = new DbUpLoggerAdapter(logger);

            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogTo(logAdapter)
                .JournalToSqlTable("dbo", "__MigrationJournal")
                .Build()
                .PerformUpgrade();

            return builder;
        }
    }

    internal sealed class DbUpLoggerAdapter : IUpgradeLog
    {
        private readonly ILogger<DbUpLoggerAdapter> _logger;

        public DbUpLoggerAdapter(ILogger<DbUpLoggerAdapter> logger)
        {
            _logger = logger;
        }

        public void WriteInformation(string format, params object[] args)
        {
            _logger.LogInformation(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            _logger.LogError(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            _logger.LogWarning(format, args);
        }
    }
}
