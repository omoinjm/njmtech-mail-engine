using Mail.Engine.Service.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Mail.Engine.Service.Application.Configuration
{
    public class ConfigurationService(IConfiguration configuration) : IConfigurationService
    {
        private readonly IConfiguration _configuration = configuration;

        public string ConnectionString() => GetConnectionString("PGSQL_CONNECTION_STRING");

        public string BlobbStorageSASUrl() => GetValue("AZURE_BLOB_STORAGE_SAS_URL")!;

        public string BlobContainerName() => GetValue("AZURE_BLOB_CONTAINER_NAME")!;

        public string TestEmailAddress() => GetValue("TEST_EMAIL_ADDRESS")!;

        public bool EmailTesting() => GetValue("EMAIL_TESTING") == "true";

        private string GetValue(string configName) => _configuration.GetValue<string>($"Values:{configName}") ?? Environment.GetEnvironmentVariable(configName)!;

        private string? GetConnectionString(string configName)
        {
            return _configuration.GetValue<string>($"ConnectionStrings:{configName}")
                ?? Environment.GetEnvironmentVariable(configName)
                ?? Environment.GetEnvironmentVariable($"ConnectionStrings:{configName}");
        }

    }
}
