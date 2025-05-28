using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Helpers;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results;
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
            var items = await _sqlContext.SelectQuery<MailboxEntity>(MailQuery.GetMailboxesQuery());

            return [.. items];
        }

        public async Task<CreateRecordResult> CreateMailMessageAsync(MessageEntity message, MailboxEntity mailbox)
        {
            var parameters = MailMessageHelper.CreateMailMessageParameters(message, mailbox);

            var result = await _sqlContext.ExecuteScalarAsyncQuery<dynamic>(MailQuery.MailboxInsertQuery(), parameters);

            if (result?.p_is_error == true) return CreateRecordResult.Error(result?.p_result_message);

            return CreateRecordResult.Successs(result!.p_return_record_id, result!.p_result_message);
        }

        public async Task CreateInReplyToAsync(string mailMessageId, string inReplyTo)
        {
            var parameters = new
            {
                p_in_reply_to_message_id = inReplyTo,
                p_mail_message_id = mailMessageId
            };

            var result = await _sqlContext.ExecuteAsyncProcQuery<dynamic>(MailQuery.InReplyToInsertQuery(), parameters);
        }

        public async Task CreateReferenceMailAsync(string mailMessageId, string reference)
        {
            var parameters = new
            {
                p_in_reply_to_message_id = reference,
                p_mail_message_id = mailMessageId
            };

            var result = await _sqlContext.ExecuteAsyncProcQuery<dynamic>(MailQuery.ReferenceInsertQuery(), parameters);
        }
        #endregion

        #region Outbound
        public async Task<List<MessageLogEntity>> GetMessageLogs()
        {
            var items = await _sqlContext.SelectQuery<MessageLogEntity>(MailQuery.GetMessageLogsQuery());

            return [.. items];
        }

        public async Task<List<MessageLogAttachmentEntity>> GetMessageAttachments(string messageLogId)
        {
            var parameters = new { p_mail_message_log_id = messageLogId };

            var items = await _sqlContext.SelectQuery<MessageLogAttachmentEntity>(MailQuery.GetMessageAttachmentsQuery(), parameters);

            return [.. items];
        }

        public async Task<bool> UpdateStatusAsync(MessageLogEntity messageLog)
        {
            var parameters = MailMessageHelper.UpdateStatusParameters(messageLog);

            var result = await _sqlContext.ExecuteAsyncQuery(MailQuery.UpdateStatusQuery(), parameters);

            return result;
        }

        public async Task<CreateRecordResult> CreateMessageLogRecord(MessageLogEntity entity)
        {
            var parameters = MailMessageHelper.CreateMailMessageLogParameters(entity);

            var result = await _sqlContext.ExecuteStoredProcedureAsync<dynamic>(
                "mail.message_log_insert",
                parameters
            );

            if (result?.p_is_error == true) return CreateRecordResult.Error(result?.p_result_message);

            return CreateRecordResult.Successs(result!.p_return_record_id, result!.p_result_message);
        }
        #endregion
    }
}
