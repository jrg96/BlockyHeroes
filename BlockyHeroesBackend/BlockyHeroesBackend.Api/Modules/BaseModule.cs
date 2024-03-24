using Asp.Versioning.Builder;
using Asp.Versioning;

namespace BlockyHeroesBackend.Api.Modules;

public class BaseModule
{
    protected RouteGroupBuilder CreateApiVersions(IEndpointRouteBuilder app, string routeName)
    {
        ApiVersionSet versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        return app
            .MapGroup("/api/v{version:apiVersion}/" + routeName)
            .WithApiVersionSet(versionSet);
    }
}
