using Core.CQRS.Abstract;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Core.CQRS.Decorators.Caching
{
    public class CachingDecorator<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>, ICachableRequest
    {
        private readonly IRequestHandler<TQuery, TResult> _commandHandler;
        private readonly IMemoryCache _memoryCache;

        public CachingDecorator(IRequestHandler<TQuery, TResult> commandHandler, IMemoryCache memoryCache)
        {
            _commandHandler = commandHandler;
            _memoryCache = memoryCache;
        }

        public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var methodName = request.GetType().FullName;
            var arguments = request.GetType().GetProperties().ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => $"{x.Name}:{x.GetValue(request)}" ?? "<Null>"))})";

            if (_memoryCache.TryGetValue<TResult>(key, out TResult result))
            {
                return result;
            }

            result = await _commandHandler.Handle(request, cancellationToken);

            _memoryCache.Set(key, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}