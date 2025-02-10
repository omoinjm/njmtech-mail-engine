using System.Net.Mail;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Core.Services.OutboundMail;

namespace Mail.Engine.Service.Infrastructure.Services.OutboundMail
{
    public class EmailBuilder(IConfigurationService config) : IEmailBuilder
    {
        private readonly IConfigurationService _config = config;


        public MailMessage BuildEmailMessage(MessageLogEntity messageLog, bool useTestEmail)
        {
            var message = new MailMessage();

            if (useTestEmail)
            {
                message.To.Add(_config.TestEmailAddress());
            }
            else
            {
                AddRecipients(message, messageLog);
            }

            SetSender(message, messageLog);
            SetContent(message, messageLog);

            return message;
        }

        private void AddRecipients(MailMessage message, MessageLogEntity messageLog)
        {
            AddAddresses(message.To, messageLog.ToField!);
            AddAddresses(message.CC, messageLog.CcField!);
            AddAddresses(message.Bcc, messageLog.BccField!);
        }

        private void AddAddresses(MailAddressCollection collection, string addressList)
        {
            if (string.IsNullOrEmpty(addressList)) return;

            foreach (var address in addressList.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                collection.Add(address.Trim());
            }
        }

        private void SetSender(MailMessage message, MessageLogEntity messageLog)
        {
            message.From = string.IsNullOrEmpty(messageLog.FromName)
                ? new MailAddress(messageLog.SmtpEmailAddress!)
                : new MailAddress(messageLog.SmtpEmailAddress!, messageLog.FromName);
        }

        private void SetContent(MailMessage message, MessageLogEntity messageLog)
        {
            message.Body = messageLog.Body;
            message.Subject = messageLog.Subject;
            message.IsBodyHtml = true;
        }
    }
}