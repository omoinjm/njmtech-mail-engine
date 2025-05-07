using Mail.Engine.Service.Core.Entities;
using MimeKit;

namespace Mail.Engine.Service.Core.Services.Mail.InboundMail
{
    public interface IMailMessageBuilder
    {
        MessageEntity BuildMailMessage(MimeMessage message);
    }
}
