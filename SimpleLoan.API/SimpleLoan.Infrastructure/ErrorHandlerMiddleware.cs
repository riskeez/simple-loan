using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SimpleLoan.Infrastructure;

/// <summary>
/// Middleware to handle exceptions with logging (and potentially, error-to-response mapping)
/// </summary>
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("error");
        }
    }
}