using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Infrastructure.DbQueries;

namespace Mail.Engine.Service.Infrastructure.Repositories
{
    public class MailRepository(ISqlSelector sqlContext) : IMailRepository
    {
        private readonly ISqlSelector _sqlContext = sqlContext;

        public async Task<List<MailboxEntity>> GetMailboxes()
        {
            var items = await _sqlContext.SelectQuery<MailboxEntity>(RepositoryQuery.GetMailboxes());

            return [.. items];
        }

        public async Task<List<MessageLogEntity>> LoadMessageLogs()
        {
            var items = await _sqlContext.SelectQuery<MessageLogEntity>(RepositoryQuery.LoadMessageLogs());

            return [.. items];
        }

        public async Task<Guid> UpsertMailMessageAsync(MessageEntity message, MailboxEntity mailbox)
        {
            var parameters = new
            {
                p_mailbox_id = mailbox.MailboxId,
                p_subject = message.Subject,
                p_text_plain = message.TextPlain,
                p_text_html = message.TextHtml,
                p_format_text_html = message.FormatTextHtml,
                p_date_sent = message.DateSent,
                p_cc_field = message.CcField,
                p_bcc_field = message.BccField,
                p_sent_to = message.SentTo,
                p_sent_from = message.SentFrom,
                p_sent_from_display_name = message.SentFromDisplayName,
                p_sent_from_raw = message.SentFromRaw,
                p_imap_message_id = message.ImapMessageId,
                p_mime_version = message.MimeVersion,
                p_return_path = message.ReturnPath,
                p_logged_in_user = "Mail Service Api",
            };

            var result = await _sqlContext.ExecuteAsyncProcQuery<dynamic>(RepositoryQuery.MailboxInsert(), parameters);

            return result;
        }

        public async Task UpsertInReplyToAsync(string mailMessageId, string inReplyTo)
        {
            var parameters = new
            {
                p_in_reply_to_message_id = inReplyTo,
                p_mail_message_id = mailMessageId
            };

            var result = await _sqlContext.ExecuteAsyncProcQuery<dynamic>(RepositoryQuery.MailboxInsert(), parameters);
        }

        public async Task UpsertReferenceMailAsync(string mailMessageId, string reference)
        {
            var parameters = new
            {
                p_in_reply_to_message_id = reference,
                p_mail_message_id = mailMessageId
            };

            var result = await _sqlContext.ExecuteAsyncProcQuery<dynamic>(RepositoryQuery.ReferenceInsert(), parameters);
        }
    }
}