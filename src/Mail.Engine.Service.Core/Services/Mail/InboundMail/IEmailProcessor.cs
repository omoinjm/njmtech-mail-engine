using Mail.Engine.Service.Core.Entities;
using MailKit;

namespace Mail.Engine.Service.Core.Services.Mail.InboundMail
{
    public interface IEmailProcessor
    {
        Task<bool> ProcessEmailAsync(
            IMessageSummary messageSummary,
            IMailFolder sourceFolder,
            IMailFolder destinationFolder,
            MailboxEntity mailbox);
    }
}
