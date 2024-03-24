using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using Microsoft.AspNetCore.Authorization;

namespace BlockyHeroesBackend.Api.Middleware.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public Roles[] Roles { get; set; }

    public RoleRequirement(params Roles[] roles)
    {
        Roles = roles;
    }
}
