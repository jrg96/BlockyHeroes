using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Infrastructure.Constants;
using System.Security.Claims;

namespace BlockyHeroesBackend.Api.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtTokenService jwtTokenService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        bool valid = jwtTokenService.ValidateToken(token);

        if (valid)
        {
            IEnumerable<Claim> claims = jwtTokenService.DecodeToken(token);

            string userId = claims
                .Where(claim => claim.Type == JwtClaimConstants.USER_ID_CLAIM)
                .Select(claim => claim.Value)
                .FirstOrDefault();

            string roleId = claims
                .Where(claim => claim.Type == JwtClaimConstants.USER_ROLE)
                .Select(claim => claim.Value)
                .FirstOrDefault();

            // If Valid reconstruct the user with the bare information we need
            Domain.Entities.User user = new Domain.Entities.User()
            {
                Id = new UserId(new Guid(userId)),
                Role = string.IsNullOrEmpty(roleId) ?
                    Roles.User
                    : Roles.GetRole(int.Parse(roleId))
            };

            // attach user to context on successful jwt validation
            context.Items["User"] = user;
        }

        await _next(context);
    }
}

public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder AddJwtValidationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<JwtMiddleware>();
        return app;
    }
}