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

        private readonly SemaphoreSlim _semaphore = new(10);

        public async Task<List<WatiApiResponse>> Handle(GetWatiQuery request, CancellationToken cancellationToken)
        {
            var result = new WatiApiResult();
            var response = new List<WatiApiResponse>();

            var watiConfig = await _repository.GetWatiConfig();

            if (watiConfig != null)
            {
                var messages = await _repository.GetMessageLogs();

                // ✅ Sort messages by oldest date first (assuming CreatedAt is the datetime field)
                var orderedMessages = messages
                    .Where(m => m != null)
                    .OrderByDescending(m => m.CreatedAt) // Replace 'CreatedAt' with the actual datetime property name
                    .ToList();

                if (orderedMessages.Count > 0)
                {
                    var tasks = orderedMessages.Select(async message =>
                    {
                        await _semaphore.WaitAsync(); // Limit concurrency by waiting for the semaphore.

                        try
                        {
                            result = await _watiService.SendMessage(message.ToField!, message.Body!);

                            response.Add(LazyMapper.Mapper.Map<WatiApiResponse>(result));

                            await _watiService.UpdateMessageStatusAsync(message, result);
                        }
                        finally
                        {
                            _semaphore.Release(); // Release the semaphore to allow other operations.
                        }
                    });

                    await Task.WhenAll(tasks);
                }
            }

            return response;
        }
    }
}
