using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Results.Wati;

namespace Mail.Engine.Service.Core.Services.Wati
{
    public interface IWatiService
    {
        Task<WatiApiResult> SendMessageTemplate(string whatsappNumber, string payload);
        Task<bool> SendMessage(string whatsappNumber, string message);


        Task UpdateMessageStatusAsync(MessageLogEntity messageLog, WatiApiResult result);
    }
}
