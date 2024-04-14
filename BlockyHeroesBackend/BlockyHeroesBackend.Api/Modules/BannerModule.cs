using BlockyHeroesBackend.Api.Middleware.Authorization;
using BlockyHeroesBackend.Application.Entities.Banner.Commands;
using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Presentation.Common;
using BlockyHeroesBackend.Presentation.RequestResponse.Banner;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlockyHeroesBackend.Api.Modules;

public class BannerModule : BaseModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder bannerGroup = CreateApiVersions(app, "banner");
        var v1 = bannerGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/pull", BannerPull)
            .Accepts<BannerPullRequest>("application/json")
            .Produces<TaskResult>(StatusCodes.Status400BadRequest)
            .Produces<TaskResult>(StatusCodes.Status403Forbidden)
            .Produces<TaskResult>(StatusCodes.Status200OK)
            .RequireAuthorization(CustomPoliciesConstants.USER_POLICY_REQUIREMENT);
    }

    /*
     * Endpoint Handlers
     */
    private async Task<IResult> BannerPull(IMediator mediator, IJwtTokenService jwtTokenService, HttpContext context, [FromBody] BannerPullRequest request)
    {
        User user = (User)context.Items["User"];

        var result = await mediator.Send(new GachaPullCommand(
            user.Id.Value,
            request.BannerId,
            request.Times));

        if (!result.Success)
        {
            return TypedResults.BadRequest(new TaskResult()
            {
                Success = false,
                Errors = result.Errors.Select(error => error.Message)
            });
        }

        return TypedResults.Ok(new TaskResult()
        {
            Success = true,
        });
    }
}
