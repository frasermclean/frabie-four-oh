using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FrabieFourOh.Services
{
    public interface IDatabaseService
    {
        Task<Database> GetDatabaseAsync();
    }

    public class DatabaseService : IDatabaseService, IDisposable
    {
        private readonly CosmosClient client;
        private readonly Lazy<Task<Database>> databaseAsync;

        public DatabaseService(ILogger<DatabaseService> logger, IConfiguration config)
        {
            var section = config.GetSection("Database");

            // read values from configuration
            string databaseId = section.GetValue<string>("Id");
            string endpointUri = section.GetValue<string>("EndpointUri");
            string primaryKey = section.GetValue<string>("PrimaryKey");

            // define client options
            CosmosClientOptions options = new()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };

            logger.LogInformation("Creating CosmosDB client.");
            client = new CosmosClient(endpointUri, primaryKey, options);

            // lazily create database object
            databaseAsync = new Lazy<Task<Database>>(async () =>
            {
                logger.LogInformation("Creating database object.");
                return await client.CreateDatabaseIfNotExistsAsync(
                  databaseId,
                  ThroughputProperties.CreateManualThroughput(400));
            });
        }

        public Task<Database> GetDatabaseAsync()
        {
            return databaseAsync.Value;
        }

        public void Dispose()
        {
            if (databaseAsync.IsValueCreated)
                databaseAsync.Value.Dispose();

            client.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
