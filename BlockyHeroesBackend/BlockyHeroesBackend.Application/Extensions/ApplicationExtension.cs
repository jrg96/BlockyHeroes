using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BlockyHeroesBackend.Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        var assembly = typeof(ApplicationExtension);

        service
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly.Assembly));

        service
            .AddValidatorsFromAssembly(assembly.Assembly);

        return service;
    }
}
