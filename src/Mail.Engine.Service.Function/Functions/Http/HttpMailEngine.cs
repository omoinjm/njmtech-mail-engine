using Mail.Engine.Service.Application.Commands;
using Mail.Engine.Service.Application.Dto;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response;
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

        #region Process Mails
        // NM: Incase something happens with the timer function
        [Function("HttpInboundMail")]
        public async Task<IActionResult> RunInboundMail(
              [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = "v1/HttpMailEngine/InboundMail")] HttpRequestData req)
        {
            _logger.LogInformation("Processing v1 request.");

            var result = await _mediator.Send(new GetInboundQuery());

            return new OkObjectResult(result);
        }

        [Function("HttpOutboundMail")]
        public async Task<IActionResult> RunOutboundMail(
              [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = "v1/HttpMailEngine/OutboundMail")] HttpRequestData req)
        {
            _logger.LogInformation("Processing v1 request.");

            var result = await _mediator.Send(new GetOutboundQuery());

            return new OkObjectResult(result);
        }
        #endregion

        [Function("HttpCreateOutboundMail")]
        public async Task<IActionResult> RunCreateOutboundMail(
              [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Post), Route = "v1/HttpMailEngine/CreateOutboundMail")] HttpRequestData req)
        {
            _logger.LogInformation("Processing v1 request.");

            var request = await BaseFunction<MessageLogDto>.RequestBody(req.Body);

            var result = await _mediator.Send(new CreateCommand<MessageLogDto, CreateResponse>(request));

            return new OkObjectResult(result);
        }
    }
}
