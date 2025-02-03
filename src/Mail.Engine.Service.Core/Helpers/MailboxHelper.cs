using Mail.Engine.Service.Core.Entities;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json.Linq;

namespace Mail.Engine.Service.Core.Helpers
{
    public class MailboxHelper
    {
        public static SaslMechanismOAuth2? GetAccessToken(MailboxEntity mailbox, params string[] scopes)
        {
            if (scopes == null || scopes.Length == 0) throw new ArgumentException("At least one scope is required", nameof(scopes));

            var scopesStr = String.Join(" ", scopes.Select(x => x?.Trim()).Where(x => !String.IsNullOrEmpty(x)));

            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", mailbox.ImapUsername!),
                new KeyValuePair<string, string>("password", mailbox.ImapPassword!),
                new KeyValuePair<string, string>("client_id", mailbox.AppId!),
                new KeyValuePair<string, string>("client_secret", mailbox.SecretId!),
                new KeyValuePair<string, string>("scope", scopesStr),
            ]);

            using var client = new HttpClient();

            var response = client.PostAsync($"https://login.microsoftonline.com/{mailbox.TenantId}/oauth2/v2.0/token", content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            var json = JObject.Parse(responseString);

            var token = json["access_token"];

            return token != null
                ? new SaslMechanismOAuth2(mailbox.ImapUsername, token.ToString())
                : null;
        }

        public static string GetPlainTextInMessage(MimeMessage message)
        {
            return message.GetTextBody(MimeKit.Text.TextFormat.Plain);
        }

        public static string GetHtmlInMessage(MimeMessage message)
        {
            return message.GetTextBody(MimeKit.Text.TextFormat.Html);
        }

        public static string GetCcList(MimeMessage message)
        {
            string cc = string.Empty;
            if (message.Headers != null)
            {
                foreach (var item in message.Cc.Mailboxes)
                {

                    if (item.Address != null && !string.IsNullOrEmpty(item.Address))
                        cc = cc + item.Address + ";";
                }

                if (cc.EndsWith(";"))
                    cc = cc.Substring(0, cc.Length - 1);
            }

            return cc;
        }

        public static string GetBccList(MimeMessage message)
        {
            string bcc = string.Empty;
            if (message.Headers != null)
            {
                foreach (var item in message.Bcc.Mailboxes)
                {
                    if (item.Address != null && !string.IsNullOrEmpty(item.Address))
                        bcc = bcc + item.Address + ";";
                }

                if (bcc.EndsWith(";"))
                    bcc = bcc.Substring(0, bcc.Length - 1);
            }

            return bcc;
        }

        public static string GetToList(MimeMessage message)
        {
            string to = string.Empty;
            if (message.Headers != null)
            {
                foreach (var item in message.To.Mailboxes)
                {
                    if (item.Address != null && !string.IsNullOrEmpty(item.Address))
                        to = to + item.Address + ";";
                }

                if (to.EndsWith(";"))
                    to = to.Substring(0, to.Length - 1);
            }

            return to;
        }
    }
}