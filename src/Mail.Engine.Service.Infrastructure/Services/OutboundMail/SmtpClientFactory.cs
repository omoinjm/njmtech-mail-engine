// using System.Net;
// using System.Net.Mail;

using MailKit.Net.Smtp;

using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Helpers;
using Mail.Engine.Service.Core.Services.OutboundMail;
using MailKit.Security;

namespace Mail.Engine.Service.Infrastructure.Services.OutboundMail
{
    public class SmtpClientFactory : ISmtpClientFactory
    {
        private const string SCOPE_EMAIL = "email";
        private const string SCOPE_OPEN_ID = "openid";
        private const string SCOPE_OFFLINE_ACCESS = "offline_access";
        private const string SCOPE_SMTP = "https://outlook.office.com/SMTP.Send";

        public SmtpClient CreateSmtpClient(MessageLogEntity settings)
        {
            var client = new SmtpClient();

            client.Connect(settings.Smtp, settings.Port.GetValueOrDefault(), SecureSocketOptions.StartTls);

            if (settings.PickupDirectoryFromIis.GetValueOrDefault())
            {
                ConfigurePickupDirectory(client, settings);
            }
            else
            {
                ConfigureSmtpServer(client, settings);
            }

            // client.EnableSsl = settings.SSL.GetValueOrDefault();

            return client;
        }

        private void ConfigurePickupDirectory(SmtpClient client, MessageLogEntity settings)
        {
            // client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            // client.PickupDirectoryLocation = settings.PickupDirectoryLocation;
        }

        private void ConfigureSmtpServer(SmtpClient client, MessageLogEntity settings)
        {

            // client.Port = settings.Port.GetValueOrDefault();
            // client.Host = settings.Smtp!;
            // client.UseDefaultCredentials = false;

            if (settings.IsOutlook)
            {
                var accessToken = MailboxHelper.GetAccessTokenSmtp(settings, SCOPE_EMAIL, SCOPE_OPEN_ID, SCOPE_OFFLINE_ACCESS, SCOPE_SMTP);

                client.Authenticate(accessToken);
            }
            else
            {
                client.Authenticate(settings.SmtpUserName, settings.SmtpPassword);
            }

            // client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // client.TargetName = "STARTTLS/smtp.server.com";
        }
    }
}
