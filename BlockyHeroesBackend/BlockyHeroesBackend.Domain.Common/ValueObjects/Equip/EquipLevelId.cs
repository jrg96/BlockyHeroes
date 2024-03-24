namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;

public readonly record struct EquipLevelId(Guid Id)
{
    public static EquipLevelId Empty => new EquipLevelId(Guid.Empty);
    public static EquipLevelId CreateEquipId()
    {
        return new EquipLevelId(Guid.NewGuid());
    }
}
