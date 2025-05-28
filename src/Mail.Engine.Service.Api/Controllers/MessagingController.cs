using System.Net;
using Mail.Engine.Service.Application.Commands;
using Mail.Engine.Service.Application.Dto;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Application.Response.Wati;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Engine.Service.Api.Controllers
{
    public class MessagingController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("Inbound")]
        [ProducesResponseType(typeof(MailResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InboundMail()
        {
            var result = await _mediator.Send(new GetInboundQuery());

            return Ok(result);
        }

        [HttpGet]
        [Route("Outbound")]
        [ProducesResponseType(typeof(MailResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> OutboundMail()
        {
            var result = await _mediator.Send(new GetOutboundQuery());

            return Ok(result);
        }

        [HttpGet]
        [Route("WatiService")]
        [ProducesResponseType(typeof(List<WatiApiResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> WatiService()
        {
            var result = await _mediator.Send(new GetWatiQuery());

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateOutboundMail")]
        [ProducesResponseType(typeof(CreateResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOutboundMail([FromBody] MessageLogDto request)
        {
            var result = await _mediator.Send(new CreateCommand<MessageLogDto, CreateResponse>(request));

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Ping")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public IActionResult Ping()
        {
            return Ok(new { message = "Pong", timestamp = DateTime.Now });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Env")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public IActionResult Env()
        {
            return Ok(new { message = $"This is a test: {Environment.GetEnvironmentVariable("TEST_VARIABLE")}", timestamp = DateTime.Now });
        }
    }
}
