using Mail.Engine.Service.Shared.Core;
using Microsoft.Extensions.Logging;

namespace Mail.Engine.Service.Shared.Services
{
    public class OutboundMailService<T>(IConfigurationService config, ILogger<T> logger) : ServiceBase<T>(logger)
    {



    }
}