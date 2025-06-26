using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response.Wati;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results.Wati;
using Mail.Engine.Service.Core.Services.Wati;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class WatiServiceHandler(
        IWatiRepository repository,
        IWatiService watiService
    ) : IRequestHandler<GetWatiQuery, List<WatiApiResponse>>
    {
        private readonly IWatiRepository _repository = repository;
        private readonly IWatiService _watiService = watiService;

        public async Task<List<WatiApiResponse>> Handle(GetWatiQuery request, CancellationToken cancellationToken)
        {
            var result = new WatiApiResult();

            var response = new List<WatiApiResponse>();

            var watiConfig = await _repository.GetWatiConfig();

            if (watiConfig != null)
            {
                var messages = await _repository.GetMessageLogs();

                if (messages != null && messages.Count > 0)
                {
                    for (int i = 0; i < messages.Count; i++)
                    {
                        result = await _watiService.SendMessage(messages[i].ToField!, messages[i].Body!);

                        response.Add(LazyMapper.Mapper.Map<WatiApiResponse>(result));

                        await _watiService.UpdateMessageStatusAsync(messages[i], result);
                    }
                }
            }

            return response;
        }
    }
}
