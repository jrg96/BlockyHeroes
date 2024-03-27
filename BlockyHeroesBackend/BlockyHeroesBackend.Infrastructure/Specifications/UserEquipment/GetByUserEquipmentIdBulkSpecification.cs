using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.UserEquipment;

public class GetByUserEquipmentIdBulkSpecification : Specification<Domain.Entities.User.UserEquipment>
{
    public GetByUserEquipmentIdBulkSpecification(IEnumerable<UserEquipmentId> userEquipmentIds)
    {
        Query
            .Include(userEquip => userEquip.Owner)
            .Include(userEquip => userEquip.EquipLevel)
            .Include(userEquip => userEquip.UserCharacterSlot1)
            .Include(userEquip => userEquip.UserCharacterSlot2)
            .Where(userEquip => userEquipmentIds.Contains(userEquip.Id));
    }
}
