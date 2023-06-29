using System.Net.Mime;

namespace Anixe.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger; //Note: logger not implemented
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
                                          RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong").ConfigureAwait(false);
            }       
        }
    }
}
