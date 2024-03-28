using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;

namespace BlockyHeroesBackend.Domain.Entities.Equip;

/// <summary>
/// Base class to represent Equipment. This class is intended to
/// be used with EquipLevel to identify all the different equipments 
/// of the game.
/// </summary>
public class Equip
{
    public EquipId Id { get; set; }
    public ItemRarity Rarity { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Foreign Key properties
    public ICollection<EquipLevel> EquipmentEvolutions { get; set; }
}
