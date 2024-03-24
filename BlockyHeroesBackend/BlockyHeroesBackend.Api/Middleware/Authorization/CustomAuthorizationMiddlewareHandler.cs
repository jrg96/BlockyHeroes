using BlockyHeroesBackend.Presentation.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Net;
using System.Text.Json;

namespace BlockyHeroesBackend.Api.Middleware.Authorization;

public class CustomAuthorizationMiddlewareHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler
         _defaultHandler = new AuthorizationMiddlewareResultHandler();

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {

        if (authorizeResult.Challenged && !authorizeResult.Succeeded && policy.Requirements.Any(requirement => requirement is RoleRequirement))
        {
            TaskResult result = new TaskResult()
            {
                Success = false,
                Errors = new List<string>() { "Forbidden" }
            };

            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(result); ;
            return;
        }

        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}
