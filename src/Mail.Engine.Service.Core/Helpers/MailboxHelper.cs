using Mail.Engine.Service.Core.Entities;
using MailKit.Security;
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
    }
}