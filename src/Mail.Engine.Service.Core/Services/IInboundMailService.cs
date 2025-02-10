using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Results;

namespace Mail.Engine.Service.Core.Services
{
    public interface IInboundMailService
    {
        Task<MailResult> GetMailsAsync(MailboxEntity mailbox);
    }
}