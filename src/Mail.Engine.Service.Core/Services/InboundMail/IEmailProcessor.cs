using Mail.Engine.Service.Core.Entities;
using MailKit;

namespace Mail.Engine.Service.Core.Services.InboundMail
{
    public interface IEmailProcessor
    {
        Task ProcessEmailAsync(
            IMessageSummary messageSummary,
            IMailFolder sourceFolder,
            IMailFolder destinationFolder,
            MailboxEntity mailbox);
    }
}