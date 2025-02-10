using System.Net.Mail;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services.OutboundMail;

namespace Mail.Engine.Service.Infrastructure.Services.OutboundMail
{
    public class EmailAttachmentProcessor(/*IAzureFileHelper azureFileHelper,*/
            IMailRepository mailRepository) : IEmailAttachmentProcessor
    {
        //  private readonly IAzureFileHelper _azureFileHelper;
        private readonly IMailRepository _mailRepository = mailRepository;

        public async Task AddAttachmentsAsync(MailMessage message, MessageLogEntity messageLog)
        {
            var attachments = await _mailRepository.GetMessageAttachments(messageLog.MessageLogId.ToString());

            foreach (var attachment in attachments)
            {
                try
                {
                    await AddSingleAttachmentAsync(message, attachment);
                }
                catch (Exception ex)
                {
                    LogAttachmentError(ex, messageLog);
                }
            }
        }

        private async Task AddSingleAttachmentAsync(MailMessage message, MessageLogAttachmentEntity attachment)
        {
            if (!string.IsNullOrEmpty(attachment.AttachmentUrl))
            {
                //         var downloadedBytes = await _azureFileHelper.DownloadFileAsync(attachment.AttachmentUrl);
                //         var data = new Attachment(new MemoryStream(downloadedBytes), attachment.FileName);
                //         message.Attachments.Add(data);
            }
            else if (attachment.AttachmentData != null)
            {
                //         var data = new Attachment(new MemoryStream(attachment.AttachmentData), attachment.FileName);
                //         message.Attachments.Add(data);
            }
        }

        private void LogAttachmentError(Exception ex, MessageLogEntity messageLog)
        {
            // _logger.LogError(new MAIL_ErrorModel
            // {
            //     MessageLogId = messageLog.MessageLogId,
            //     SmtpId = messageLog.SmtpConfigurationId,
            //     Message = ex.Message,
            //     StackTrace = ex.StackTrace,
            //     Area = "SMTP",
            //     Function = "AddAttachments",
            //     Source = ex.Source,
            //     HelpLink = ex.HelpLink,
            //     Engine = "Outgoing",
            //     Step = "Attempting to add attachments"
            // });
        }
    }
}