using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Services.InboundMail;
using MimeKit;

namespace Mail.Engine.Service.Infrastructure.Services.InboundMail
{
    public class AttachmentProcessor(/* IAzureStorageHelper storageHelper*/) : IAttachmentProcessor
    {
        // private readonly IAzureStorageHelper _storageHelper;

        public async Task ProcessAttachmentsAsync(MimeMessage message, string mailMessageId, MailboxEntity mailbox)
        {
            await ProcessEmbeddedAttachmentsAsync(message, mailMessageId, mailbox);
            await ProcessRegularAttachmentsAsync(message, mailMessageId, mailbox);
        }

        private async Task ProcessEmbeddedAttachmentsAsync(MimeMessage message, string mailMessageId, MailboxEntity mailbox)
        {
            var embeddedAttachments = message.BodyParts.OfType<MimePart>()
                .Where(x => x.ContentType.IsMimeType("image", "*"));

            foreach (var attachment in embeddedAttachments)
            {
                try
                {
                    await ProcessSingleAttachmentAsync(attachment, mailMessageId, true, mailbox);
                }
                catch (Exception ex)
                {
                    LogAttachmentError(ex, mailMessageId, mailbox, "Embedded Attachment");
                }
            }
        }

        private async Task ProcessRegularAttachmentsAsync(MimeMessage message, string mailMessageId, MailboxEntity mailbox)
        {
            // int emailIndex = 0;

            foreach (var attachment in message.Attachments)
            {
                try
                {
                    if (attachment is MimePart mimePart)
                    {
                        await ProcessSingleAttachmentAsync(mimePart, mailMessageId, false, mailbox);
                    }
                    else if (attachment is MessagePart messagePart)
                    {
                        // await ProcessMessagePartAttachmentAsync(messagePart, mailMessageId, emailIndex++, mailbox);
                    }
                }
                catch (Exception ex)
                {
                    LogAttachmentError(ex, mailMessageId, mailbox, "Regular Attachment");
                }
            }
        }

        private async Task ProcessSingleAttachmentAsync(
            MimePart attachment,
            string mailMessageId,
            bool isEmbedded,
            MailboxEntity mailbox)
        {
            using var memory = new MemoryStream();
            await attachment.Content.DecodeToAsync(memory);
            var bytes = memory.ToArray();

            // var folderName = _config.InboundEmailContainerName() ?? "dev";
            // var fileName = GetFileName(attachment, isEmbedded);

            // await _storageHelper.UploadAttachmentBlobAsync(
            //     bytes,
            //     mailMessageId,
            //     fileName,
            //     "mailmessageattachments",
            //     folderName);

            // var blobUrl = await _storageHelper.GetBlobStoredAccessSignatureAsync(
            //     mailMessageId,
            //     fileName,
            //     "mailmessageattachments",
            //     folderName);

            // await SaveAttachmentToDatabase(
            //     mailMessageId,
            //     blobUrl,
            //     fileName,
            //     attachment,
            //     isEmbedded);
        }

        private void LogAttachmentError(Exception ex, string mailMessageId, MailboxEntity mailbox, string step)
        {
            // logger.LogError(ex, $"Error processing attachment for mail message {mailMessageId} in mailbox {mailbox.Name}. Step: {step}");
            // _logger.LogError(new MAIL_ErrorModel
            // {
            //     MailMessageId = mailMessageId,
            //     MailBoxId = mailbox.MailboxId,
            //     SmtpId = mailbox.SmtpId,
            //     ImapId = mailbox.ImapId,
            //     Message = ex.Message,
            //     StackTrace = ex.StackTrace,
            //     Area = "IMAP",
            //     Function = "SaveMailToDatabase",
            //     Source = ex.Source,
            //     HelpLink = ex.HelpLink,
            //     Engine = "Incoming",
            //     Step = $"Saving {step}"
            // });
        }
    }
}