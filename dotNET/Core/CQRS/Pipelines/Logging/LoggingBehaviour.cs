using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Core.CQRS.Pipelines.Logging
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggingBehaviour(IHttpContextAccessor httpContextAccessor,
                   ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string? user = _httpContextAccessor.HttpContext.User.Identity?.Name == null ?
                           "?" : _httpContextAccessor.HttpContext.User.Identity.Name;

            _logger.LogInformation($"Request type: {request.GetType().Name}\n" +
                                   $"Request: {JsonConvert.SerializeObject(request)}\n" +
                                   $"User: {user}\n");

            var response = await next();

            //Response
            _logger.LogInformation($"Handled {JsonConvert.SerializeObject(response)}");

            return response;
        }
    }
}
