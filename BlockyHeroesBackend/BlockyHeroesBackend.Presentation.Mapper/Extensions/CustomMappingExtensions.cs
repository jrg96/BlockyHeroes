using BlockyHeroesBackend.Presentation.Mapper.Mappings.User;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlockyHeroesBackend.Presentation.Mapper.Extensions;

public static class CustomMappingExtensions
{
    public static IServiceCollection AddCustomMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(UserMapperConfig).Assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
