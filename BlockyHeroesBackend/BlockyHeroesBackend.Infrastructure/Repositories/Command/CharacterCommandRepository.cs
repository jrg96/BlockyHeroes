using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class CharacterCommandRepository : GenericCommandRepository<Character>, ICharacterCommandRepository
{
    public CharacterCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
