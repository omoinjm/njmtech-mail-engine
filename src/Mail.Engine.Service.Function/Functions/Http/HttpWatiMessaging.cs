using Mail.Engine.Service.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Mail.Engine.Service.Function.Functions.Http;

public class HttpWatiMessaging(ILogger<HttpWatiMessaging> logger, IMediator mediator)
{
    private readonly ILogger<HttpWatiMessaging> _logger = logger;
    private readonly IMediator _mediator = mediator;

    [Function("HttpWatiMessaging")]
    public async Task<IActionResult> RunWatiMessaging([HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get))] HttpRequest req)
    {
        _logger.LogInformation("Processing v1 request.");

        var result = await _mediator.Send(new GetWatiQuery());

        return new OkObjectResult(result);
    }

    [Function("HttpCustomerLogout")]
    public async Task<IActionResult> RunCustomerLogout([HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get))] HttpRequest req)
    {
        _logger.LogInformation("Processing v1 request.");

        var result = await _mediator.Send(new GetCustomerLogoutQuery());

        return new OkObjectResult(result);
    }
}
