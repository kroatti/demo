using Delta.Invoicing.Core.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Delta.Invoicing.Core.Pipeline
{
    class RequestValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<RequestLogging<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest>? _validator;

        public RequestValidator(ILogger<RequestLogging<TRequest, TResponse>> logger,
            IValidator<TRequest>? validator = null)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(request, cancellationToken);

                if (!result.IsValid)
                {
                    _logger.LogInformation("{req} failed due to validation", request.GetType().GetNameConcat());
                    throw new ValidationException(result.Errors);
                }
            }

            return await next();
        }
    }
}