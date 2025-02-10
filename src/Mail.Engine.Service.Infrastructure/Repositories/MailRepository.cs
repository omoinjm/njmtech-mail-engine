using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Helpers;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Infrastructure.DbQueries;

namespace Mail.Engine.Service.Infrastructure.Repositories
{
    public class MailRepository(ISqlSelector sqlContext) : IMailRepository
    {
        private readonly ISqlSelector _sqlContext = sqlContext;

        #region Inbound
        public async Task<List<MailboxEntity>> GetMailboxes()
        {
            var items = await _sqlContext.SelectQuery<MailboxEntity>(RepositoryQuery.GetMailboxesQuery());

            return [.. items];
        }

        public async Task<Guid> CreateMailMessageAsync(MessageEntity message, MailboxEntity mailbox)
        {
            var parameters = MailMessageHelper.CreateMailMessageParameters(message, mailbox);

            return await _sqlContext.ExecuteScalarAsyncQuery<Guid>(RepositoryQuery.MailboxInsertQuery(), parameters);
        }

        public async Task CreateInReplyToAsync(string mailMessageId, string inReplyTo)
        {
            var parameters = new
            {
                p_in_reply_to_message_id = inReplyTo,
                p_mail_message_id = mailMessageId
            };

            var result = await _sqlContext.ExecuteAsyncProcQuery<dynamic>(RepositoryQuery.InReplyToInsertQuery(), parameters);
        }

        public async Task CreateReferenceMailAsync(string mailMessageId, string reference)
        {
            var parameters = new
            {
                p_in_reply_to_message_id = reference,
                p_mail_message_id = mailMessageId
            };

            var result = await _sqlContext.ExecuteAsyncProcQuery<dynamic>(RepositoryQuery.ReferenceInsertQuery(), parameters);
        }
        #endregion

        #region Outbound
        public async Task<List<MessageLogEntity>> GetMessageLogs()
        {
            var items = await _sqlContext.SelectQuery<MessageLogEntity>(RepositoryQuery.GetMessageLogsQuery());

            return [.. items];
        }

        public async Task<List<MessageLogAttachmentEntity>> GetMessageAttachments(string messageLogId)
        {
            var parameters = new { p_mail_message_log_id = messageLogId };

            var items = await _sqlContext.SelectQuery<MessageLogAttachmentEntity>(RepositoryQuery.GetMessageAttachmentsQuery(), parameters);

            return [.. items];
        }

        public async Task<bool> UpdateStatusAsync(MessageLogEntity messageLog)
        {
            var parameters = new
            {
                p_mail_message_log_id = messageLog.MessageLogId,
                p_mail_message_log_status_code = messageLog.MessageLogStatusCode,
                p_mail_date_sent = messageLog.DateSent,
                p_mail_status_message = messageLog.StatusMessage,
                p_mail_from_field = messageLog.FromField,
                p_mail_from_name = messageLog.FromName
            };

            var result = await _sqlContext.ExecuteAsyncQuery(RepositoryQuery.UpdateStatusQuery(), parameters);

            return result;
        }
        #endregion
    }
}