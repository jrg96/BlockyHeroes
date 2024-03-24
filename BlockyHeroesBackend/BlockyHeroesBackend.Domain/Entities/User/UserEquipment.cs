using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Equip;

namespace BlockyHeroesBackend.Domain.Entities.User;

public class UserEquipment
{
    public UserEquipmentId Id { get; set; }
    public int Quantity { get; set; }

    // Foreign Key relationships
    public UserId UserId { get; set; }
    public User Owner { get; set; }

    public EquipLevelId EquipLevelId { get; set; }
    public EquipLevel EquipLevel { get; set; }
}
