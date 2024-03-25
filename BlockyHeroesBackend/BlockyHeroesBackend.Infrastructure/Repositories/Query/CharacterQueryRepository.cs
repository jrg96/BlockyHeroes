using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class CharacterQueryRepository : GenericQueryRepository<CharacterQueryRepository>, ICharacterQueryRepository
{
    public CharacterQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
