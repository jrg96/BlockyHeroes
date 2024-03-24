
using BlockyHeroesBackend.Presentation.Common;
using FluentValidation;

namespace BlockyHeroesBackend.Api.Middleware;

public class ExceptionMiddleware
{
    private RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception e)
        {
            await HandleException(context, e);
        }
    }

    private async Task HandleException(HttpContext context, Exception e)
    {
        TaskResult result = new TaskResult()
        {
            Success = false,
        };

        if (e is ValidationException)
        {
            result.Errors = ((ValidationException)e)
                .Errors
                .Select(error => error.ErrorMessage)
                .ToList();
        }
        else
        {
            result.Errors = new List<string>() { e.ToString() };
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(result);
    }
}

public static class CustomExceptionExtensions
{
    public static IApplicationBuilder AddCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}
