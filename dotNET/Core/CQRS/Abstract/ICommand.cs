using MediatR;

namespace Core.CQRS.Abstract
{
    public interface ICommand<TResponse> : IRequest<TResponse>
    { }
}
