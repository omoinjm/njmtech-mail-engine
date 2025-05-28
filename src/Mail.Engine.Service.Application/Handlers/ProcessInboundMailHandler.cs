using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results;
using Mail.Engine.Service.Core.Services.Mail;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class ProcessInboundMailHandler(
        IMailRepository repository,
        IInboundMailService mailService

    ) : IRequestHandler<GetInboundQuery, MailResponse>
    {
        private readonly IMailRepository _repository = repository;
        private readonly IInboundMailService _mailService = mailService;

        public async Task<MailResponse> Handle(GetInboundQuery request, CancellationToken cancellationToken)
        {
            var result = new MailResult();

            var mailboxes = await _repository.GetMailboxes();

            foreach (var mailbox in mailboxes)
            {
                var mailboxResult = await _mailService.GetMailsAsync(mailbox);
                result.TotalMessagesProcessed += mailboxResult.TotalMessagesProcessed;
                result.TotalMessagesFailed += mailboxResult.TotalMessagesFailed;
                result.ErrorMessages.AddRange(mailboxResult.ErrorMessages);
            }

            var response = LazyMapper.Mapper.Map<MailResponse>(result);

            return response;
        }

    }
}
