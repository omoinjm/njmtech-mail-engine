using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response.Wati;
using Mail.Engine.Service.Core.Enum;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results.Wati;
using Mail.Engine.Service.Core.Services.Wati;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class WatiServiceHandler(
        IMailRepository mailRepository,
        IWatiRepository watiRepository,
        IWatiService watiService
    ) : IRequestHandler<GetWatiQuery, List<WatiApiResponse>>
    {
        private readonly IMailRepository _mailRepository = mailRepository;
        private readonly IWatiRepository _watiRepository = watiRepository;
        private readonly IWatiService _watiService = watiService;

        public async Task<List<WatiApiResponse>> Handle(GetWatiQuery request, CancellationToken cancellationToken)
        {
            var result = new WatiApiResult();
            var response = new List<WatiApiResponse>();

            var watiConfig = await _watiRepository.GetWatiConfig();

            if (watiConfig != null)
            {
                var messageList = _mailRepository.GetMessageLogs().Result.Where(x => x.MessageLogTypeName == EnumMessageTypeLog.WATI).ToList();

                if (messageList != null && messageList.Count > 0)
                {
                    int emailCount = 0;

                    foreach (var message in messageList)
                    {
                        emailCount++;

                        result = await _watiService.SendMessage(message.ToField!, message.Body!);

                        response.Add(LazyMapper.Mapper.Map<WatiApiResponse>(result));
                    }
                }
            }

            return response;
        }
    }
}
