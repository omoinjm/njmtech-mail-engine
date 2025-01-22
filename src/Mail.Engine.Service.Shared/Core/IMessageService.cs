using Mail.Engine.Service.Shared.Models;

namespace Mail.Engine.Service.Shared.Core
{
    public interface IMessageService
    {
        MessageResult SendMessage(string number, string message);
        Task<MessageResult> SendMessageAsync(string number, string message);
    }
}