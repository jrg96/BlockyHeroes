using BlockyHeroesBackend.Application.Entities.User.Commands;
using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Infrastructure.Constants;
using BlockyHeroesBackend.Presentation.Common;
using BlockyHeroesBackend.Presentation.RequestResponse.User.Request;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockyHeroesBackend.Api.Modules;

public class AuthenticationModule : BaseModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder authGroup = CreateApiVersions(app, "auth");
        var v1 = authGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/login", LoginUser)
            .Accepts<LoginUserRequest>("application/json")
            .Produces<TaskResult>(StatusCodes.Status403Forbidden)
            .Produces<TaskResult>(StatusCodes.Status400BadRequest)
            .Produces<TaskResult<string>>(StatusCodes.Status200OK);
    }

    /*
     * Endpoint Handlers
     */
    private async Task<IResult> LoginUser(IMediator mediator, IJwtTokenService jwtTokenService, [FromBody] LoginUserRequest request)
    {
        var result = await mediator.Send(new LoginUserCommand(
            request.Id, request.Password));

        if (!result.Success)
        {
            return TypedResults.BadRequest(new TaskResult()
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Message)
            });
        }

        // Generate token if login was successful
        Dictionary<string, string> claims = new Dictionary<string, string>()
        {
            { JwtClaimConstants.USER_ID_CLAIM, result.Data.Id.ToString() }
        };

        return TypedResults.Ok(new TaskResult<string>()
        {
            Success = false,
            Data = jwtTokenService.GenerateToken(claims)
        });
    }
}
