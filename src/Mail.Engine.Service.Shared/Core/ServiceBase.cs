using Microsoft.Extensions.Logging;

namespace Mail.Engine.Service.Shared.Core
{
    public abstract class ServiceBase<T>(ILogger<T> logger)
    {
        private readonly ILogger<T> _logger = logger;

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogError(string message)
        {
            _logger.LogError($"An error occured: {message}");
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning($"An error occured: {message}");
        }
    }
}