using Restaurants.Domain.Exceptions;
using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWatch.Stop();

        if (stopWatch.ElapsedMilliseconds / 4 > 4)
        {
            logger.LogInformation("Request [{Verb}] at {Patch} took {Time}ms", 
                context.Request.Method,
                context.Request.Path,
                stopWatch.ElapsedMilliseconds);
        }
    }
}
