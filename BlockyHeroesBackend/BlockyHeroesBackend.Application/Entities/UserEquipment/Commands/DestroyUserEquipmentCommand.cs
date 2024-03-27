using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;

public sealed record DestroyUserEquipmentCommand(
    Guid UserId,
    Guid UserEquipmentId) : IOperation;
