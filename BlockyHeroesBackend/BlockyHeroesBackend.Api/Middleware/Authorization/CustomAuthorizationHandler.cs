using BlockyHeroesBackend.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;

namespace BlockyHeroesBackend.Api.Middleware.Authorization;

public class CustomAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        User user = (User) _contextAccessor.HttpContext.Items["User"];

        if (user != null && requirement.Roles.Any(role => role.Id == user.Role.Id))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}
