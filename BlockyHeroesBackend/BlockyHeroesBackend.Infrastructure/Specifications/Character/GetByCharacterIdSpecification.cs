using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;

namespace BlockyHeroesBackend.Infrastructure.Specifications.Character;

public class GetByCharacterIdSpecification : Specification<Domain.Entities.Character.Character>
{
    public GetByCharacterIdSpecification(CharacterId characterId)
    {
        Query
            .Include(character => character.CharacterLevels)
            .ThenInclude(characterLevel => characterLevel.CharacterLevelRequirements)
            .ThenInclude(charLevelReq => charLevelReq.Item)
            .Where(character => character.Id == characterId);
    }
}
