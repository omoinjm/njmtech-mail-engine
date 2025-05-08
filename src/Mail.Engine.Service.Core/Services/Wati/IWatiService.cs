using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Results.Wati;

namespace Mail.Engine.Service.Core.Services.Wati
{
    public interface IWatiService
    {
        Task<WatiApiResult> SendMessage(string whatsappNumber, string payload);

        Task UpdateMessageStatusAsync(MessageLogEntity messageLog, WatiApiResult result);
    }
}
