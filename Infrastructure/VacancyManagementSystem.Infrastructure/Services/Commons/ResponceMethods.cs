using System.Text.Json;
using VacancyManagementSystem.Application.Responses;

namespace VacancyManagementSystem.Infrastructure.Services.Commons;
public abstract class ResponceMethods
{
    public Response Success(object? content = null)
    {
        return new Response(200, content: content);
    }

    public Response BadRequest(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(400, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response NotFound(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(404, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response Unauthorized(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(401, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response Forbidden(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(403, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response Error(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(500, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response UnprocessableEntity(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(422, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response Conflict(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(409, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response NoContent()
    {
        return new Response(204);
    }

    public Response MethodNotAllowed(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(405, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }

    public Response RequestTimeout(string exceptionMessage, string? innerExceptionMessage = null)
    {
        return new Response(408, exceptionMessage: exceptionMessage, innerExceptionMessage: innerExceptionMessage);
    }
}
