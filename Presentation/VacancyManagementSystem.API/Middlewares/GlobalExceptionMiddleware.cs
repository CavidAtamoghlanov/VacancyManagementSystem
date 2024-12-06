using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;
using VacancyManagementSystem.Application.Responses;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var response = new Response(
                statusCode: 500,
                exceptionMessage: ex.Message,
                innerExceptionMessage: ex.InnerException?.Message,
                content: null
            );

            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(response);
        }

        if (!httpContext.Response.HasStarted)
        {
            if (httpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
                httpContext.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                var modelState = httpContext.RequestServices.GetService(typeof(ModelStateDictionary)) as ModelStateDictionary;

                if (modelState != null && !modelState.IsValid)
                {
                    var errors = modelState.Values
                        .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    var response = new Response(
                        statusCode: 400,
                        exceptionMessage: "One or more validation errors occurred.",
                        content: new { Errors = errors }
                    );

                    httpContext.Response.StatusCode = 400;
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}
