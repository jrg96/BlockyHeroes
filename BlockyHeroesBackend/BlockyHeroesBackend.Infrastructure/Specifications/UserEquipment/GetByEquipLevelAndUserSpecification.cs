using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.UserEquipment;

public class GetByEquipLevelAndUserSpecification : Specification<Domain.Entities.User.UserEquipment>
{
    public GetByEquipLevelAndUserSpecification(UserId userId, EquipLevelId equipLevelId)
    {
        Query
            .Include(userEquip => userEquip.Owner)
            .Where(userEquip => userEquip.UserId == userId
                && userEquip.EquipLevelId == equipLevelId);
    }
}
