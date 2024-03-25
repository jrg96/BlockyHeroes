using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class CharacterLevelRequirementCommandRepository : GenericCommandRepository<CharacterLevelRequirement>, ICharacterLevelRequirementCommandRepository
{
    public CharacterLevelRequirementCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
