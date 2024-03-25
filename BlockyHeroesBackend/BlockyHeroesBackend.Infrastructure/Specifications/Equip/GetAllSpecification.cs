using Ardalis.Specification;

namespace BlockyHeroesBackend.Infrastructure.Specifications.Equip;

public class GetAllSpecification : Specification<Domain.Entities.Equip.Equip>
{
    public GetAllSpecification()
    {
        Query
            .Include(equip => equip.EquipmentEvolutions);
    }
}
