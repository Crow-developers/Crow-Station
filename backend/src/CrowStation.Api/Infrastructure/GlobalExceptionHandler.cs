using Microsoft.AspNetCore.Diagnostics;

namespace CrowStation.Api.Infrastructure;

/// <summary>
/// Turns unexpected exceptions into RFC 9457 problem details without leaking internals.
/// </summary>
internal sealed partial class GlobalExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        LogUnhandledException(logger, exception);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails =
            {
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
            },
        }).ConfigureAwait(false);
    }

    [LoggerMessage(Level = LogLevel.Error, Message = "Unhandled exception")]
    private static partial void LogUnhandledException(ILogger logger, Exception exception);
}
