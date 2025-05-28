using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Core.Services.Mail;
using Mail.Engine.Service.Core.Services.Mail.OutboundMail;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class ProcessOutboundMailHandler(
        IMailRepository repository,
        IConfigurationService config,

        IOutboundMailService mailService,
        IEmailBuilder emailBuilder,
        IEmailAttachmentProcessor attachmentProcessor

    ) : IRequestHandler<GetOutboundQuery, MailResponse>
    {
        private readonly IMailRepository _repository = repository;
        private readonly IConfigurationService _config = config;

        private readonly IOutboundMailService _mailService = mailService;
        private readonly IEmailBuilder _emailBuilder = emailBuilder;
        private readonly IEmailAttachmentProcessor _attachmentProcessor = attachmentProcessor;


        public async Task<MailResponse> Handle(GetOutboundQuery request, CancellationToken cancellationToken)
        {
            var result = new MailResult();

            var emailList = _repository.GetMessageLogs().Result;

            if (emailList != null && emailList.Count > 0)
            {
                int emailCount = 0;

                foreach (var messageLog in emailList)
                {
                    await _mailService.ExcludeMessageswhileProcessing(messageLog);

                    emailCount++;

                    using var message = _emailBuilder.BuildEmailMessage(messageLog, _config.EmailTesting());

                    // await _attachmentProcessor.AddAttachmentsAsync(message, messageLog);

                    result = await _mailService.SendEmailAsync(message, messageLog);

                    await _mailService.UpdateMessageStatusAsync(messageLog, result.IsSuccess);
                }
            }

            var response = LazyMapper.Mapper.Map<MailResponse>(result);

            return response;
        }
    }
}
