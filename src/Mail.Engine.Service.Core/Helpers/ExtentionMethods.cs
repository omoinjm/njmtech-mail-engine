using System.Text.Json;
using Mail.Engine.Service.Core.Utils;
using MimeKit;

namespace Mail.Engine.Service.Core.Helpers
{
    public static class ExtentionMethods
    {
        public static string GetPlainTextInMessage(this MimeMessage message) =>
        message.GetTextBody(MimeKit.Text.TextFormat.Plain);

        public static string GetHtmlInMessage(this MimeMessage message) =>
            message.GetTextBody(MimeKit.Text.TextFormat.Html);

        public static string GetCcList(this MimeMessage message) =>
            GetEmailList(message.Cc.Mailboxes);

        public static string GetBccList(this MimeMessage message) =>
            GetEmailList(message.Bcc.Mailboxes);

        public static string GetToList(this MimeMessage message) =>
            GetEmailList(message.To.Mailboxes);

        private static string GetEmailList(IEnumerable<MailboxAddress> mailboxes)
        {
            if (mailboxes == null)
                return string.Empty;

            return string.Join(";",
                mailboxes
                    .Where(m => !string.IsNullOrEmpty(m?.Address))
                    .Select(m => m.Address));
        }

        public static bool IsValidPhoneNumber(this string phoneNumber, string alpha3Code)
        {
            return alpha3Code switch
            {
                "ZAF" => PhoneNumberValidator.IsValidSouthAfricanCellNumber(phoneNumber),
                "GBR" => PhoneNumberValidator.IsValidUkMobileNumber(phoneNumber),
                "ZWE" => PhoneNumberValidator.IsValidZimbabweMobileNumber(phoneNumber),
                _ => false
            };
        }

        public static bool IsValidJson(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            try
            {
                JsonDocument.Parse(input);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
