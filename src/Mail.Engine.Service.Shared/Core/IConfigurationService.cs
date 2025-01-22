namespace Mail.Engine.Service.Shared.Core
{
    public interface IConfigurationService
    {
        string ConnectionString();
        string BlobbStorageSASUrl();
        string BlobContainerName();
    }
}