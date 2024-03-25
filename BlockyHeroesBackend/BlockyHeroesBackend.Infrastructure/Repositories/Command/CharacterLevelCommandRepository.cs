using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class CharacterLevelCommandRepository : GenericCommandRepository<CharacterLevel>, ICharacterLevelCommandRepository
{
    public CharacterLevelCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
