using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Infrastructure.Specifications.CharacterLevel;

public class GetByCharacterLevelAndGachaTypeSpecification : Specification<Domain.Entities.Character.CharacterLevel>
{
    public GetByCharacterLevelAndGachaTypeSpecification(int level, GachaType gachaType)
    {
        Query
            .Include(characterLevel => characterLevel.Character)
            .Where(characterLevel => characterLevel.Level == level
                && characterLevel.Character.GachaType == gachaType) ;
    }
}
