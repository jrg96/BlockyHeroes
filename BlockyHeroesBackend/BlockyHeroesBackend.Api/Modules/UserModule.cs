using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;

namespace BlockyHeroesBackend.Api.Modules;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder userGroup = CreateApiVersions(app);
        var v1 = userGroup.MapGroup("").HasApiVersion(1);

        v1.MapPost("/", CreateUser);
    }

    private RouteGroupBuilder CreateApiVersions(IEndpointRouteBuilder app)
    {
        ApiVersionSet versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        return app
            .MapGroup("/api/v{version:apiVersion}/user")
            .WithApiVersionSet(versionSet);
    }

    private async Task<IResult> CreateUser()
    {
        return TypedResults.Ok("Hello World");
    }
}
