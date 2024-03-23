﻿using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;
using BlockyHeroesBackend.Application.Services;

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

    private async Task<IResult> CreateUser(IJwtTokenService jwtTokenService)
    {
        var testDict = new Dictionary<string, string>() 
        {
            { "Username", "TestValue" }
        };

        var token = jwtTokenService.GenerateToken(testDict);

        var test = jwtTokenService.DecodeToken(token);

        return TypedResults.Ok("Hello World");
    }
}
