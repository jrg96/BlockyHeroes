using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.UserCharacter;

public class GetByUserCharacterIdSpecification : Specification<Domain.Entities.User.UserCharacter>
{
    public GetByUserCharacterIdSpecification(UserCharacterId id)
    {
        Query
            .Include(userChar => userChar.Owner)
            .Include(userChar => userChar.CharacterLevel)
            .Where(userChar => userChar.Id == id);
    }
}
