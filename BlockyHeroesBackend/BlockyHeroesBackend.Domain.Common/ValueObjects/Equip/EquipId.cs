namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;

public readonly record struct EquipId(Guid Id)
{
    public static EquipId Empty => new EquipId(Guid.Empty);
    public static EquipId CreateEquipId()
    {
        return new EquipId(Guid.NewGuid());
    }
}
