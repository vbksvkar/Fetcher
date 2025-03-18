using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Fetcher.Api.Infra;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<IExceptionHandler> logger;

    public GlobalExceptionHandler(ILogger<IExceptionHandler> logger)
    {
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        this.logger.LogError(exception, "An unhandled exception occurred.");
        var problemDetails = new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}