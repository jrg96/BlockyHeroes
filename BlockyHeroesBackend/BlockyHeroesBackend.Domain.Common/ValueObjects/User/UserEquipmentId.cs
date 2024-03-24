namespace BlockyHeroesBackend.Domain.Common.ValueObjects.User;

public readonly record struct UserEquipmentId(Guid Value)
{
    public static UserEquipmentId Empty => new UserEquipmentId(Guid.Empty);
    public static UserEquipmentId CreateEquipmentId()
    {
        return new UserEquipmentId(Guid.NewGuid());
    }
}
