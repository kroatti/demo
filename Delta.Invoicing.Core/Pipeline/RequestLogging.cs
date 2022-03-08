using System.Diagnostics;
using Delta.Invoicing.Core.Extensions;
using Delta.Invoicing.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Delta.Invoicing.Core.Pipeline
{
    class RequestLogging<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<RequestLogging<TRequest, TResponse>> _logger;

        public RequestLogging(ILogger<RequestLogging<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            _logger.LogActivityStart($"Handling {typeof(TRequest).GetNameConcat()}", out Activity a);
            try
            {
                response = await next();
            }
            finally
            {
                _logger.LogActivityEnd(a);
            }

            return response;
        }
    }
}