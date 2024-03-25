using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class CharacterLevelRequirementQueryRepository : GenericQueryRepository<CharacterLevelRequirement>, ICharacterLevelRequirementQueryRepository
{
    public CharacterLevelRequirementQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
