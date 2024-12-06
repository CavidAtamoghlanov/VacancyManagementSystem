namespace VacancyManagementSystem.Application.Responses;
public class Response
{
    public int StatusCode { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? InnerExceptionMessage { get; set; }
    public object? Content { get; set; }

    public Response(int statusCode, string? exceptionMessage = null, string? innerExceptionMessage = null, object? content = null)
    {
        StatusCode = statusCode;
        ExceptionMessage = exceptionMessage;
        InnerExceptionMessage = innerExceptionMessage;
        Content = content;
    }
}
