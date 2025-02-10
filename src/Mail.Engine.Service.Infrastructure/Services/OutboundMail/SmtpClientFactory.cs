using System.Net;
using System.Net.Mail;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Services.OutboundMail;

namespace Mail.Engine.Service.Infrastructure.Services.OutboundMail
{
    public class SmtpClientFactory : ISmtpClientFactory
    {
        public SmtpClient CreateSmtpClient(MessageLogEntity settings)
        {
            var client = new SmtpClient();

            if (settings.PickupDirectoryFromIis.GetValueOrDefault())
            {
                ConfigurePickupDirectory(client, settings);
            }
            else
            {
                ConfigureSmtpServer(client, settings);
            }

            client.EnableSsl = settings.SSL.GetValueOrDefault();

            return client;
        }

        private void ConfigurePickupDirectory(SmtpClient client, MessageLogEntity settings)
        {
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation = settings.PickupDirectoryLocation;
        }

        private void ConfigureSmtpServer(SmtpClient client, MessageLogEntity settings)
        {
            client.Port = settings.Port.GetValueOrDefault();
            client.Host = settings.Smtp!;

            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(settings.SmtpUserName, settings.SmtpPassword);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.TargetName = "STARTTLS/smtp.server.com";
        }
    }
}