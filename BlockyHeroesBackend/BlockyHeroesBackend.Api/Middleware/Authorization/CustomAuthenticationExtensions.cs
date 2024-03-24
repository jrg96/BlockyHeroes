using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace BlockyHeroesBackend.Api.Middleware.Authorization;

public static class CustomAuthenticationExtensions
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareHandler>();
        services.AddAuthentication(o =>
        {
            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.RequireAuthenticatedSignIn = false;
        })
        .AddCookie();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(CustomPoliciesConstants.USER_POLICY_REQUIREMENT, policy =>
            {
                policy.AddRequirements(new RoleRequirement(Roles.Admin, Roles.User));
            });
        });

        return services;
    }
}
