using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SimpleLoan.Application.Exceptions;

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

            // can be done via exceptionToResponseMapper
            ExceptionResponse exResponse = e switch
            {
                // Customize exception here
                AppException ex => new ExceptionResponse(HttpStatusCode.BadRequest, ex.Code, ex.Message),
                _ => new ExceptionResponse(HttpStatusCode.BadRequest, "unexpected_error")
            };
            
            var jsonResponse = JsonSerializer.Serialize(new {code = exResponse.Code, message = exResponse.Reason });
            context.Response.StatusCode = (int) exResponse.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}