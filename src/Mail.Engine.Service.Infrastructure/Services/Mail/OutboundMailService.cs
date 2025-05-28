using System.Net.Mail;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Enum;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Core.Services.Mail;
using Mail.Engine.Service.Core.Services.Mail.OutboundMail;

namespace Mail.Engine.Service.Infrastructure.Services
{
    public class OutboundMailService(
        IMailRepository repository,
        ISmtpClientFactory smtpClientFactory

    ) : IOutboundMailService
    {
        private readonly ISmtpClientFactory _smtpClientFactory = smtpClientFactory;
        private readonly IMailRepository _repository = repository;

        public async Task<MailResult> SendEmailAsync(MailMessage message, MessageLogEntity messageLog)
        {
            var result = new MailResult();

            try
            {
                using var client = _smtpClientFactory.CreateSmtpClient(messageLog);

                await client.SendAsync((MimeKit.MimeMessage)message); //SendMailAsync(message);

                result.TotalMessagesProcessed++;
            }
            catch (Exception ex)
            {
                // LogSmtpError(ex, messageLog);

                result.TotalMessagesFailed++;
                result.ErrorMessages.Add(ex.Message);
            }

            return result;
        }

        public async Task UpdateMessageStatusAsync(MessageLogEntity messageLog, bool sent)
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

        public async Task ExcludeMessageswhileProcessing(MessageLogEntity messageLog)
        {
            messageLog.MessageLogStatusCode = EnumMessageStatusLog.Created;

            await _repository.UpdateStatusAsync(messageLog);
        }
    }
}
