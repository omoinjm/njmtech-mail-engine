using System.Net.Mail;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Results;

namespace Mail.Engine.Service.Core.Services.Mail
{
    public interface IOutboundMailService
    {
        Task<MailResult> SendEmailAsync(MailMessage message, MessageLogEntity messageLog);

        Task UpdateMessageStatusAsync(MessageLogEntity messageLog, bool sent);

        Task ExcludeMessageswhileProcessing(MessageLogEntity messageLog);
    }
}
