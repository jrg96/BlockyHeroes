using Ardalis.Specification;

namespace BlockyHeroesBackend.Infrastructure.Specifications.Character;

public class GetAllSpecification : Specification<Domain.Entities.Character.Character>
{
    public GetAllSpecification()
    {
        Query
            .Include(character => character.CharacterLevels);
    }
}
