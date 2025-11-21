using AgileBoard.Core.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AgileBoard.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred while processing request {RequestPath}", context.Request.Path);
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            CustomException customEx => (
                StatusCodes.Status400BadRequest,
                new Error(
                    customEx.GetType().Name.Underscore().Replace("_exception", string.Empty),
                    customEx.Message
                )
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                new Error("error", "There was an error.")
            ),
        };

        _logger.LogWarning(
            "Returning {StatusCode} error response: {ErrorCode} - {ErrorReason}",
            statusCode,
            error.Code,
            error.Reason
        );

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private record Error(string Code, string Reason);
}