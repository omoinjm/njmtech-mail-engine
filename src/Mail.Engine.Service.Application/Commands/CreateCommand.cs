using MediatR;

namespace Mail.Engine.Service.Application.Commands
{
    public class CreateCommand<T, R>(T item) : IRequest<R> where R : class
    {
        public T Item { get; set; } = item;
    }
}
