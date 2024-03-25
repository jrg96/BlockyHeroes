using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Presentation.Common;
using BlockyHeroesBackend.Presentation.RequestResponse.UserEquipment;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlockyHeroesBackend.Api.Modules;

public class UserEquipmentModule : BaseModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = base.CreateApiVersions(app, "user/equipment");
        var v1 = group.MapGroup("").HasApiVersion(1);

        v1.MapPost("/upgrade", UpgradeEquipment)
            .Accepts<UpgradeUserEquipmentRequest>("application/json")
            .Produces<TaskResult>(StatusCodes.Status403Forbidden)
            .Produces<TaskResult>(StatusCodes.Status400BadRequest);

    }

    /*
     * Endpoint Handlers
     */
    private async Task<IResult> UpgradeEquipment(IMediator mediator, HttpContext context, [FromBody] UpgradeUserEquipmentRequest request)
    {
        // Decode Json Token
        User user = (User)context.Items["User"];
        var result = await mediator.Send(new UpgradeEquipmentCommand(
            user.Id.Value,
            request.Equipment,
            request.Levels
            ));

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
