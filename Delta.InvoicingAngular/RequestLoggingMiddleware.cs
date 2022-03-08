using System.Diagnostics;

namespace Delta.InvoicingAngular
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var act = new Activity("Request " + context.Request.Path.Value).Start();
            try
            {
                await _next(context);
            }
            finally
            {
                act.Stop();
                _logger.LogInformation(
                    "{method} {url} [{statusCode}] {elapsed:N} ms",
                    context.Request.Method,
                    context.Request.Path.Value,
                    context.Response.StatusCode,
                    act.Duration.TotalMilliseconds);
            }
        }
    }
}
