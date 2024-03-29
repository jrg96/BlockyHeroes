using BlockyHeroesBackend.Application.Abstractions;

namespace BlockyHeroesBackend.Application.Entities.Banner.Commands;

public sealed record GachaPullCommand(
    Guid UserId,
    Guid GachaBannerId,
    int NumberOfPulls) : IOperation<IEnumerable<Domain.Entities.User.UserCharacter>>;
