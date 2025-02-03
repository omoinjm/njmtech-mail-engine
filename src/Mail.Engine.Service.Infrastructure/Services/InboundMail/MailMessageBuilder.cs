using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Helpers;
using Mail.Engine.Service.Core.Services.InboundMail;
using MimeKit;

namespace Mail.Engine.Service.Infrastructure.Services.InboundMail
{
    public class MailMessageBuilder : IMailMessageBuilder
    {
        public MessageEntity BuildMailMessage(MimeMessage message)
        {
            var mailMessage = new MessageEntity
            {
                Subject = message.Subject,
                DateSent = message.Date.LocalDateTime,
                CcField = MailboxHelper.GetCcList(message),
                BccField = MailboxHelper.GetBccList(message),
                SentTo = MailboxHelper.GetToList(message),
                TextHtml = MailboxHelper.GetHtmlInMessage(message),
                TextPlain = MailboxHelper.GetPlainTextInMessage(message),
                ImapMessageId = message.MessageId,
                MimeVersion = message.MimeVersion.ToString()
            };

            if (message.From != null)
            {
                var fromMailbox = message.From.Mailboxes.FirstOrDefault();

                if (fromMailbox != null)
                {
                    mailMessage.SentFrom = fromMailbox.Address;
                    mailMessage.SentFromDisplayName = fromMailbox.Name;
                }
            }

            return mailMessage;
        }
    }
}