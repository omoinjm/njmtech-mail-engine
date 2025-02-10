using Mail.Engine.Service.Core.Entities;

namespace Mail.Engine.Service.Core.Repositories
{
    public interface IMailRepository
    {

        Task<List<MailboxEntity>> GetMailboxes();
        Task<List<MessageLogEntity>> GetMessageLogs();
        Task<List<MessageLogAttachmentEntity>> GetMessageAttachments(string messageLogId);

        Task<Guid> CreateMailMessageAsync(MessageEntity message, MailboxEntity mailbox);
        Task CreateInReplyToAsync(string mailMessageId, string inReplyTo);
        Task CreateReferenceMailAsync(string mailMessageId, string reference);

        Task<bool> UpdateStatusAsync(MessageLogEntity messageLog);
    }
}