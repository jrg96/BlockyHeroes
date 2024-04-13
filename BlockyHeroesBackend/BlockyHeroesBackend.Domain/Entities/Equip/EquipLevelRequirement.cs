using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;

namespace BlockyHeroesBackend.Domain.Entities.Equip;

public class EquipLevelRequirement
{
    public EquipLevelRequirementId Id { get; set; }
    public int Quantity { get; set; }

    // Foreign Key configurations
    public EquipLevelId EquipLevelId { get; set; }
    public EquipLevel EquipLevel { get; set; }

    public ItemId ItemId { get; set; }
    public Item.Item Item { get; set; }
}
