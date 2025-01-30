using Mail.Engine.Service.Shared.Core;
using Microsoft.Extensions.Logging;

namespace Mail.Engine.Service.Shared.Services
{
    public class InboundMailService<T>(IConfigurationService config, ILogger<T> logger) : ServiceBase<T>(logger)
    {
        const string AREA = "IMAP";
        const string SCOPE_EMAIL = "email";
        const string SCOPE_OPEN_ID = "openid";
        const string SCOPE_OFFLINE_ACCESS = "offline_access";
        const string SCOPE_SMTP = "https://outlook.office.com/SMTP.Send";

        private readonly IConfigurationService _config = config;


    }
}