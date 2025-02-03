using MediatR;

namespace Mail.Engine.Service.Application.Commands
{
    public class CreateOutboundCommand : IRequest<bool> { }
}