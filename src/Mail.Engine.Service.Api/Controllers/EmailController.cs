using System.Net;
using Mail.Engine.Service.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Engine.Service.Api.Controllers
{
    public class EmailController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("InboundMails")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendEmail()
        {
            var result = await _mediator.Send(new GetInboundQuery());

            return Ok(result);
        }
    }
}