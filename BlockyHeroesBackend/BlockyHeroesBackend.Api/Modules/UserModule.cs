using Carter;
using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Api.Middleware.Authorization;
using BlockyHeroesBackend.Presentation.Common;
using MediatR;
using BlockyHeroesBackend.Application.Entities.User.Commands;
using BlockyHeroesBackend.Presentation.RequestResponse.User.Request;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using BlockyHeroesBackend.Presentation.RequestResponse.User.Dto;
using Mapster;

namespace BlockyHeroesBackend.Api.Modules;

public class UserModule : BaseModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder userGroup = CreateApiVersions(app, "user");
        var v1 = userGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/", CreateUser)
            .Produces<TaskResult>(StatusCodes.Status403Forbidden)
            .Produces<TaskResult>(StatusCodes.Status400BadRequest)
            .Produces<TaskResult<UserDto>>(StatusCodes.Status200OK);
    }

    private async Task<IResult> CreateUser(IMediator mediator, IJwtTokenService jwtTokenService, IMapper mapper, [FromBody] CreateUserRequest createUserRequest)
    {
        var result = await mediator.Send(new CreateUserCommand());

        if (!result.Success)
        {
            return TypedResults.BadRequest(new TaskResult()
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Message)
            });
        }

        return TypedResults.Ok(new TaskResult<UserDto>()
        {
            Success = true,
            Data = result.Data.Adapt<UserDto>()
        });
    }
}
