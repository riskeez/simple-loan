using System.Net;

namespace SimpleLoan.Application.Exceptions;

public class ExceptionResponse
{
    public ExceptionResponse(HttpStatusCode statusCode, string code, string? reason = null)
    {
        StatusCode = statusCode;
        Code = code;
        Reason = reason;
    }
        
    public HttpStatusCode StatusCode { get; set; }
    public string Code { get; set; }
    public string? Reason { get; set; }
}