using BlockyHeroesBackend.Application.Abstractions;

namespace BlockyHeroesBackend.Application.Entities.UserCharacter.Commands;

public sealed record UpgradeUserCharacterCommand(
    Guid UserId,
    Guid UserCharacterId,
    int LevelsToUpgrade) : IOperation;