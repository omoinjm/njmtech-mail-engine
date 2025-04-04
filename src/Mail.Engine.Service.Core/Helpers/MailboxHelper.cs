using Mail.Engine.Service.Core.Entities;
using MailKit.Security;
using Newtonsoft.Json.Linq;

namespace Mail.Engine.Service.Core.Helpers
{
    public class MailboxHelper
    {
        public static SaslMechanismOAuth2? GetAccessTokenImap(MailboxEntity mailbox, params string[] scopes)
        {
            return GetAccessTokenInternal(
                username: mailbox.ImapUsername!,
                password: mailbox.ImapPassword!,
                clientId: mailbox.AppId!,
                clientSecret: mailbox.SecretId!,
                tenantId: mailbox.TenantId!,
                scopes: scopes
            );
        }

        public static SaslMechanismOAuth2? GetAccessTokenSmtp(MessageLogEntity messageLog, params string[] scopes)
        {
            return GetAccessTokenInternal(
                username: messageLog.SmtpUserName!,
                password: messageLog.SmtpPassword!,
                clientId: messageLog.AppId!,
                clientSecret: messageLog.SecretId!,
                tenantId: messageLog.TenantId!,
                scopes: scopes
            );
        }

        private static SaslMechanismOAuth2? GetAccessTokenInternal(
            string username,
            string password,
            string clientId,
            string clientSecret,
            string tenantId,
            params string[] scopes)
        {
            if (scopes == null || scopes.Length == 0)
                throw new ArgumentException("At least one scope is required", nameof(scopes));

            var scopesStr = string.Join(" ", scopes.Select(x => x?.Trim()).Where(x => !string.IsNullOrEmpty(x)));

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", scopesStr),
            });

            using var client = new HttpClient();

            var response = client.PostAsync($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            var json = JObject.Parse(responseString);
            var token = json["access_token"];

            return token != null ? new SaslMechanismOAuth2(username, token.ToString()) : null;
        }
    }
}
