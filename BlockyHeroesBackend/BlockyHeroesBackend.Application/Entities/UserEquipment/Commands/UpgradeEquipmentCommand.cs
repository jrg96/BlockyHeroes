using BlockyHeroesBackend.Application.Abstractions;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;

public sealed record UpgradeEquipmentCommand(
    Guid UserId,
    Guid EquipLevelId,
    int Levels
) : IOperation;