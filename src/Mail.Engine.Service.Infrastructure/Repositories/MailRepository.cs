using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Infrastructure.DbQueries;
using Mail.Engine.Service.Infrastructure.Helpers;

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
            var parameters = MailMessageHelper.CreateMailMessageParameters(message, mailbox);

            return await _sqlContext.ExecuteScalarAsyncQuery<Guid>(RepositoryQuery.MailboxInsert(), parameters);
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