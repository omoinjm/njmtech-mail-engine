using System.Net.Mail;
using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Enum;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Core.Services.OutboundMail;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class ProcessOutboundMailHandler(
        IMailRepository repository,
        IEmailBuilder emailBuilder,
        IEmailAttachmentProcessor attachmentProcessor,
        ISmtpClientFactory smtpClientFactory,
        IConfigurationService config

    ) : IRequestHandler<GetOutboundQuery, MailResponse>
    {
        private readonly IEmailBuilder _emailBuilder = emailBuilder;
        private readonly IEmailAttachmentProcessor _attachmentProcessor = attachmentProcessor;
        private readonly ISmtpClientFactory _smtpClientFactory = smtpClientFactory;
        private readonly IMailRepository _repository = repository;
        private readonly IConfigurationService _config = config;

        public async Task<MailResponse> Handle(GetOutboundQuery request, CancellationToken cancellationToken)
        {
            var result = new MailResult();

            var emailList = _repository.GetMessageLogs().Result;

            if (emailList != null && emailList.Count > 0)
            {
                int emailCount = 0;

                foreach (var messageLog in emailList)
                {
                    emailCount++;

                    using var message = _emailBuilder.BuildEmailMessage(messageLog, _config.EmailTesting());
                    // await _attachmentProcessor.AddAttachmentsAsync(message, messageLog);

                    result = await SendEmailAsync(message, messageLog);

                    await UpdateMessageStatusAsync(messageLog, result.IsSuccess);
                }
            }

            var response = LazyMapper.Mapper.Map<MailResponse>(result);

            return response;
        }

        private async Task<MailResult> SendEmailAsync(MailMessage message, MessageLogEntity messageLog)
        {
            var result = new MailResult();

            try
            {
                using var client = _smtpClientFactory.CreateSmtpClient(messageLog);

                await client.SendMailAsync(message);

                result.TotalMessagesProcessed++;
            }
            catch (Exception ex)
            {
                LogSmtpError(ex, messageLog);

                result.TotalMessagesFailed++;
                result.ErrorMessages.Add(ex.Message);
            }

            return result;
        }

        private async Task UpdateMessageStatusAsync(MessageLogEntity messageLog, bool sent)
        {
            if (sent)
            {
                messageLog.StatusMessage = "Successful";
                messageLog.DateSent = DateTime.Now;
                messageLog.MessageLogStatusCode = EnumMessageStatusLog.Sent;
            }
            else
            {
                messageLog.StatusMessage = "Failed to send";
                messageLog.MessageLogStatusCode = EnumMessageStatusLog.Failed;
            }

            await _repository.UpdateStatusAsync(messageLog);
        }

        private void LogSendError(Exception ex, MessageLogEntity messageLog)
        {
            // _logger.LogError(new MAIL_ErrorModel
            // {
            //     MessageLogId = messageLog.MessageLogId,
            //     SmtpId = messageLog.SmtpConfigurationId,
            //     Message = ex.Message,
            //     StackTrace = ex.StackTrace,
            //     Area = "SMTP",
            //     Function = "SendMail",
            //     Source = ex.Source,
            //     HelpLink = ex.HelpLink,
            //     Engine = "Outgoing",
            //     Step = "Creating MailMessage object"
            // });
        }

        private void LogSmtpError(Exception ex, MessageLogEntity messageLog)
        {
            // _logger.LogError(new MAIL_ErrorModel
            // {
            //     MessageLogId = messageLog.MessageLogId,
            //     SmtpId = messageLog.SmtpConfigurationId,
            //     Message = ex.Message,
            //     StackTrace = ex.StackTrace,
            //     Area = "SMTP",
            //     Function = "Send",
            //     Source = ex.Source,
            //     HelpLink = ex.HelpLink,
            //     Engine = "Outgoing",
            //     Step = "SMTP Client attempting to send"
            // });
        }
    }
}