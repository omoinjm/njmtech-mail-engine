namespace Mail.Engine.Service.Core.Services
{
    public interface IConfigurationService
    {
        string ConnectionString();
        string BlobbStorageSASUrl();
        string BlobContainerName();
    }
}