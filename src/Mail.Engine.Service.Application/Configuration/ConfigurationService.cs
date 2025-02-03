using Mail.Engine.Service.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Mail.Engine.Service.Application.Configuration
{
    public class ConfigurationService(IConfiguration configuration) : IConfigurationService
    {
        private readonly IConfiguration _configuration = configuration;

        public string ConnectionString()
        {
            return Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING")
                ?? GetConnectionString("PGSQL_CONNECTION_STRING");
        }

        public string BlobbStorageSASUrl()
        {
            return Environment.GetEnvironmentVariable("AZURE_BLOB_STORAGE_SAS_URL")
                ?? GetValue("AZURE_BLOB_STORAGE_SAS_URL")!;
        }

        public string BlobContainerName()
        {
            return Environment.GetEnvironmentVariable("AZURE_BLOB_CONTAINER_NAME")
                ?? GetValue("AZURE_BLOB_CONTAINER_NAME")!;
        }

        private string GetValue(string configName)
        {
            return _configuration.GetValue<string>($"Values:{configName}")!;
        }

        private string GetConnectionString(string configName)
        {
            return _configuration.GetValue<string>($"ConnectionStrings:{configName}")!;
        }
    }
}