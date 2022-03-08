using Delta.Invoicing.Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Delta.InvoicingAngular
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;

        public ExceptionFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundException:
                    context.Result = new NotFoundResult();
                    break;
                
                case ValidationException exception:
                    context.Result = new BadRequestObjectResult(exception.ToModelState());
                    break;
                
                case Exception exception:
                    context.Result = new ObjectResult(new {Error = exception.Message}) {StatusCode = 500};
                    _loggerFactory.CreateLogger(GetType()).LogError(exception, "Internal server error");
                    break;
            }
        }
    }
}
