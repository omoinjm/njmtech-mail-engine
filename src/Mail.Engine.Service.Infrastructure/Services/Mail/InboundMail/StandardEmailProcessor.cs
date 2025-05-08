using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services.Mail.InboundMail;
using MailKit;
using MimeKit;

namespace Mail.Engine.Service.Infrastructure.Services.InboundMail
{
    public class StandardEmailProcessor(IMailMessageBuilder messageBuilder,
            IAttachmentProcessor attachmentProcessor,
            IMailRepository mailRepository) : IEmailProcessor
    {
        private readonly IMailMessageBuilder _messageBuilder = messageBuilder;
        private readonly IAttachmentProcessor _attachmentProcessor = attachmentProcessor;
        private readonly IMailRepository _mailRepository = mailRepository;

        public async Task<bool> ProcessEmailAsync(
            IMessageSummary messageSummary,
            IMailFolder sourceFolder,
            IMailFolder destinationFolder,
            MailboxEntity mailbox)
        {
            var message = await sourceFolder.GetMessageAsync(messageSummary.UniqueId);
            bool success = await SaveMailToDatabaseAsync(message, mailbox);

            if (success)
            {
                await sourceFolder.AddFlagsAsync(messageSummary.UniqueId, MessageFlags.Seen, true);
                await sourceFolder.MoveToAsync(messageSummary.UniqueId, destinationFolder);
            }

            return success;
        }

        private async Task<bool> SaveMailToDatabaseAsync(MimeMessage message, MailboxEntity mailbox)
        {
            var mailMessage = _messageBuilder.BuildMailMessage(message);

            var mailMessageId = await _mailRepository.CreateMailMessageAsync(mailMessage, mailbox);

            if (mailMessageId == Guid.Empty)
                return false;

            // await _attachmentProcessor.ProcessAttachmentsAsync(message, mailMessageId, mailbox);

            // if (message.InReplyTo != null)
            // {
            //     await _mailRepository.CreateInReplyToAsync(mailMessageId, message.InReplyTo);
            // }

            // if (message.References != null)
            // {
            //     foreach (var reference in message.References)
            //     {
            //         await _mailRepository.CreateReferenceMailAsync(mailMessageId, reference);
            //     }
            // }

            return true;
        }
    }
}
