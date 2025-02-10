using Mail.Engine.Service.Application.Response;
using MediatR;

namespace Mail.Engine.Service.Application.Queries
{
    public class GetOutboundQuery : IRequest<MailResponse> { }
}