using VacancyManagementSystem.Application.Responses;

namespace VacancyManagementSystem.Infrastructure.Helpers;

public static class ResponseHelper
{
    public static Response Success(string content = null)
    {
        return new Response(200, content: content);
    }

    public static Response Failure(string errorMessage)
    {
        return new Response(400, exceptionMessage: errorMessage);
    }

    public static Response Error(string errorMessage)
    {
        return new Response(500, exceptionMessage: errorMessage);
    }
}
