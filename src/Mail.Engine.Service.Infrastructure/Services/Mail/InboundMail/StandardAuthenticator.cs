using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Services.Mail.InboundMail;
using MailKit.Net.Imap;

namespace Mail.Engine.Service.Infrastructure.Services.InboundMail
{
    public class StandardAuthenticator : IEmailAuthenticator
    {
        public async Task AuthenticateAsync(IImapClient client, MailboxEntity mailbox)
        {
            await client.AuthenticateAsync(mailbox.ImapUsername, mailbox.ImapPassword);
        }
    }
}
