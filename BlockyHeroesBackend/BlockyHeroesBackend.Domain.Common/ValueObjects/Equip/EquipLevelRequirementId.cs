namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;

public readonly record struct EquipLevelRequirementId(Guid Id)
{
    public static EquipLevelRequirementId Empty => new EquipLevelRequirementId(Guid.Empty);

    public static EquipLevelRequirementId CreateEquipLevelRequirementId()
    {
        return new EquipLevelRequirementId(Guid.NewGuid());
    }
}
