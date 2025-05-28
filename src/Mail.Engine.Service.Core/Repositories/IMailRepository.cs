using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Results;

namespace Mail.Engine.Service.Core.Repositories
{
    public interface IMailRepository
    {
        Task<List<MailboxEntity>> GetMailboxes();
        Task<List<MessageLogEntity>> GetMessageLogs();
        Task<List<MessageLogAttachmentEntity>> GetMessageAttachments(string messageLogId);

        Task CreateInReplyToAsync(string mailMessageId, string inReplyTo);
        Task CreateReferenceMailAsync(string mailMessageId, string reference);
        Task<CreateRecordResult> CreateMailMessageAsync(MessageEntity message, MailboxEntity mailbox);

        Task<bool> UpdateStatusAsync(MessageLogEntity messageLog);

        // NM: This will replace the way I created Outbound Mails
        Task<CreateRecordResult> CreateMessageLogRecord(MessageLogEntity entity);
    }
}
