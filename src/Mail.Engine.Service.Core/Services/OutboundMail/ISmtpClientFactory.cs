// using System.Net.Mail;
using MailKit.Net.Smtp;
using Mail.Engine.Service.Core.Entities;

namespace Mail.Engine.Service.Core.Services.OutboundMail
{
    public interface ISmtpClientFactory
    {
        SmtpClient CreateSmtpClient(MessageLogEntity settings);
    }
}
