using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Repositories.Query;
using Microsoft.Extensions.DependencyInjection;

namespace BlockyHeroesBackend.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        // Adding Repositories
        service.AddScoped<IUserCommandRepository, UserCommandRepository>();
        service.AddScoped<IUserQueryRepository, UserQueryRepository>();

        return service;
    }
}
