using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Helpers;
using Mail.Engine.Service.Core.Services.InboundMail;
using MailKit.Net.Imap;

namespace Mail.Engine.Service.Infrastructure.Services.InboundMail
{
    public class OutlookAuthenticator : IEmailAuthenticator
    {
        private const string SCOPE_EMAIL = "email";
        private const string SCOPE_OPEN_ID = "openid";
        private const string SCOPE_OFFLINE_ACCESS = "offline_access";
        private const string SCOPE_IMAP = "https://outlook.office.com/IMAP.AccessAsUser.All";

        public async Task AuthenticateAsync(IImapClient client, MailboxEntity mailbox)
        {
            var accessToken = MailboxHelper.GetAccessToken(mailbox, SCOPE_EMAIL, SCOPE_OPEN_ID, SCOPE_OFFLINE_ACCESS, SCOPE_IMAP);

            await client.AuthenticateAsync(accessToken);
        }
    }
}