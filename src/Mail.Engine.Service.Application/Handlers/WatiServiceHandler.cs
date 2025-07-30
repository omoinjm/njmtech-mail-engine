using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response.Wati;
using Mail.Engine.Service.Core.Helpers;
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
            var response = new List<WatiApiResponse>();

            var watiConfig = await _repository.GetWatiConfig();

            if (watiConfig != null)
            {
                var messages = await _repository.GetMessageLogs();

                if (messages != null && messages.Count > 0)
                {
                    foreach (var message in messages)
                    {
                        try
                        {
                            WatiApiResult result;

                            if (message.Body!.IsValidJson())
                            {
                                result = await _watiService.SendMessageTemplate(message.ToField!, message.Body!);
                            }
                            else
                            {
                                var isSuccess = await _watiService.SendMessage(message.ToField!, message.Body!);
                                result = isSuccess
                                    ? new WatiApiResult { Result = true, PhoneNumber = message.ToField! }
                                    : new WatiApiResult { Result = false, PhoneNumber = message.ToField! };
                            }

                            response.Add(LazyMapper.Mapper.Map<WatiApiResponse>(result));

                            await _watiService.UpdateMessageStatusAsync(message, result);
                        }
                        catch (Exception ex)
                        {
                            // Log the error - replace with your preferred logging system
                            Console.WriteLine($"Error processing message to {message.ToField}: {ex.Message}");

                            // Optionally, you can record the failed attempt as well:
                            var failedResult = new WatiApiResult
                            {
                                Result = false,
                                PhoneNumber = message.ToField!,
                                // ErrorMessage = ex.Message
                            };

                            response.Add(LazyMapper.Mapper.Map<WatiApiResponse>(failedResult));
                        }
                    }
                }
            }

            return response;
        }
    }
}
