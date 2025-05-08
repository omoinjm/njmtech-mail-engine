using Mail.Engine.Service.Application.Response.Wati;
using MediatR;

namespace Mail.Engine.Service.Application.Queries
{
    public class GetWatiQuery : IRequest<List<WatiApiResponse>> { }
}
