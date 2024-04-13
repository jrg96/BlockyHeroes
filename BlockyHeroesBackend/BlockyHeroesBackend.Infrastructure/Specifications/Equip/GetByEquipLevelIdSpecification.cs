using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;

namespace BlockyHeroesBackend.Infrastructure.Specifications.Equip;

public class GetByEquipLevelIdSpecification : Specification<Domain.Entities.Equip.Equip>
{
    public GetByEquipLevelIdSpecification(EquipLevelId equipLevelId)
    {
        Query
            .Include(equip => equip.EquipmentEvolutions)
                .ThenInclude(equipLevel => equipLevel.EquipLevelRequirements)
                    .ThenInclude(requirement => requirement.Item)
            .Where(equip => 
                equip.EquipmentEvolutions
                    .Any(equipLevel => equipLevel.Id == equipLevelId));
    }
}
