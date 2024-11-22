using System.Diagnostics;

namespace Company.RestApi.Middleware
{

    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Incoming request: {Method} {Path} with Query {QueryString}",
                context.Request.Method, context.Request.Path, context.Request.QueryString);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during the request.");
                throw;
            }
            finally
            {
                stopwatch.Stop();

                _logger.LogInformation("Outgoing response: {StatusCode} for {Method} {Path} (Elapsed Time: {ElapsedMilliseconds}ms)",
                    context.Response.StatusCode, context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}

