using BlockyHeroesBackend.Application.Entities.UserCharacter.Commands;
using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Presentation.Common;
using BlockyHeroesBackend.Presentation.RequestResponse.UserCharacter;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlockyHeroesBackend.Api.Modules;

public class UserCharacterModule : BaseModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder userCharGroup = CreateApiVersions(app, "user/character");
        var v1 = userCharGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/upgrade", UpgradeUserCharacter)
            .Accepts<UpgradeUserCharacterRequest>("application/json")
            .Produces<TaskResult>(StatusCodes.Status400BadRequest)
            .Produces<TaskResult>(StatusCodes.Status403Forbidden)
            .Produces<TaskResult>(StatusCodes.Status200OK);
    }

    /*
     * Endpoint Handlers
     */
    private async Task<IResult> UpgradeUserCharacter(IMediator mediator, IJwtTokenService jwtTokenService, HttpContext context, [FromBody] UpgradeUserCharacterRequest request)
    {
        User user = (User)context.Items["User"];

        var result = await mediator.Send(new UpgradeUserCharacterCommand(
            user.Id.Value,
            request.UserCharacterId,
            request.LevelsToUpgrade));

        if (!result.Success)
        {
            return TypedResults.BadRequest(new TaskResult()
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Message)
            });
        }

        return TypedResults.Ok(new TaskResult()
        {
            Success = true,
        });
    }
}
