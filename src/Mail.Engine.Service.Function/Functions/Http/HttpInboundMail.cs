using Mail.Engine.Service.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Mail.Engine.Service.Function.Functions.Http
{
    public class HttpInboundMail(ILogger<HttpInboundMail> logger, IMediator mediator)
    {
        private readonly ILogger<HttpInboundMail> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [Function("HttpInboundMail")]
        public async Task<IActionResult> Run(
              [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get))] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetInboundQuery());

            return new OkObjectResult(result);
        }
    }
}
