using MediatR;

namespace Core.CQRS.Abstract
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    { }
}
