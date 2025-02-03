using Mail.Engine.Service.Core.Entities;

namespace Mail.Engine.Service.Core.Repositories
{
    public interface IMailRepository
    {

        Task<List<MailboxEntity>> GetMailboxes();
        Task<List<MessageLogEntity>> LoadMessageLogs();

        Task<Guid> UpsertMailMessageAsync(MessageEntity message, MailboxEntity mailbox);
        Task UpsertInReplyToAsync(string mailMessageId, string inReplyTo);
        Task UpsertReferenceMailAsync(string mailMessageId, string reference);
    }
}