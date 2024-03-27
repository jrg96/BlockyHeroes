using BlockyHeroesBackend.Application.Abstractions;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;

public sealed record AssignUserEquipmentCommand(
    Guid UserId,
    Guid UserCharacterId,
    Guid UserEquipmentId,
    int SlotToEquip) : IOperation;
