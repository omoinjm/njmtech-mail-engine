using System.Net;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Engine.Service.Api.Controllers
{
    public class EmailController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("Inbound")]
        [ProducesResponseType(typeof(MailResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InboundMails()
        {
            var result = await _mediator.Send(new GetInboundQuery());

            return Ok(result);
        }

        [HttpGet]
        [Route("Outbound")]
        [ProducesResponseType(typeof(MailResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Outbound()
        {
            var result = await _mediator.Send(new GetOutboundQuery());

            return Ok(result);
        }
    }
}