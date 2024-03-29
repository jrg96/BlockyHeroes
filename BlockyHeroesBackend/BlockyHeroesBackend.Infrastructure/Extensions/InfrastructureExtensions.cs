using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Repositories;
using BlockyHeroesBackend.Infrastructure.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlockyHeroesBackend.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        // Adding Infrastructure services
        service.AddScoped<IUserSecurityService, UserSecurityService>();
        service.AddScoped<IJwtTokenService, JwtTokenService>();

        // Adding Repositories
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IUserCommandRepository, UserCommandRepository>();
        service.AddScoped<IUserQueryRepository, UserQueryRepository>();

        service.AddScoped<IUserEquipmentCommandRepository, UserEquipmentCommandRepository>();
        service.AddScoped<IUserEquipmentQueryRepository, UserEquipmentQueryRepository>();

        service.AddScoped<IEquipCommandRepository, EquipCommandRepository>();
        service.AddScoped<IEquipQueryRepository, EquipQueryRepository>();

        service.AddScoped<IItemCommandRepository, ItemCommandRepository>();
        service.AddScoped<IItemQueryRepository, ItemQueryRepository>();

        service.AddScoped<IUserItemCommandRepository, UserItemCommandRepository>();
        service.AddScoped<IUserItemQueryRepository, UserItemQueryRepository>();

        service.AddScoped<ICharacterCommandRepository, CharacterCommandRepository>();
        service.AddScoped<ICharacterQueryRepository, CharacterQueryRepository>();

        service.AddScoped<ICharacterLevelCommandRepository, CharacterLevelCommandRepository>();
        service.AddScoped<ICharacterLevelQueryRepository, CharacterLevelQueryRepository>();

        service.AddScoped<ICharacterLevelRequirementCommandRepository, CharacterLevelRequirementCommandRepository>();
        service.AddScoped<ICharacterLevelRequirementQueryRepository, CharacterLevelRequirementQueryRepository>();

        service.AddScoped<IUserCharacterCommandRepository, UserCharacterCommandRepository>();
        service.AddScoped<IUserCharacterQueryRepository, UserCharacterQueryRepository>();

        service.AddScoped<IGachaBannerCommandRepository, GachaBannerCommandRepository>();
        service.AddScoped<IGachaBannerQueryRepository, GachaBannerQueryRepository>();

        service.AddScoped<IBannerDropRateCommandRepository, BannerDropRateCommandRepository>();
        service.AddScoped<IBannerDropRateQueryRepository, BannerDropRateQueryRepository>();

        service.AddScoped<IGachaBannerCharacterCommandRepository, GachaBannerCharacterCommandRepository>();
        service.AddScoped<IGachaBannerCharacterQueryRepository, GachaBannerCharacterQueryRepository>();

        return service;
    }
}
