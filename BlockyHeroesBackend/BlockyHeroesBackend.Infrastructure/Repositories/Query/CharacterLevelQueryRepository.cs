using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class CharacterLevelQueryRepository : GenericQueryRepository<CharacterLevel>, ICharacterLevelQueryRepository
{
    public CharacterLevelQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
