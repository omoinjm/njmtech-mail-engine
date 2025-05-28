using System.Text.Json;
using Mail.Engine.Service.Application.Queries;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Mail.Engine.Service.Function.Functions.Timer;

public class TimerWatiMessaging(ILogger<TimerMailEngine> logger, IMediator mediator)
{
    private readonly ILogger<TimerMailEngine> _logger = logger;
    private readonly IMediator _mediator = mediator;

    [Function("TimerWatiMessaging")]
    public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }

        var result = await _mediator.Send(new GetWatiQuery());

        _logger.LogInformation(JsonSerializer.Serialize(result));
    }
}
