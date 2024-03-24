using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Domain.Entities.Equip;

/// <summary>
/// Class used to represent the stat gain/debuff for a specific level
/// of a equip.
/// </summary>
public class EquipLevel
{
    public EquipLevelId Id { get; set; }
    public int Level { get; set; }
    public long CoinsToPromote { get; set; }
    public int Lives { get; set; }
    public float JumpForce { get; set; }
    public float HorizontalSpeed { get; set; }

    // Foreign key properties
    public EquipId EquipId { get; set; }
    public Equip Equip { get; set; }

    public ICollection<UserEquipment> UserEquipment { get; set; }
}
