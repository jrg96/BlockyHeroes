using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.CharacterLevel;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class CharacterLevelQueryRepository : GenericQueryRepository<CharacterLevel>, ICharacterLevelQueryRepository
{
    public CharacterLevelQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<(CharacterLevelId, ItemRarity)>> GetByCharacterLevelAndGachaTypeAsync(int level, GachaType gachaType)
    {
        return (await Find(new GetByCharacterLevelAndGachaTypeSpecification(level, gachaType))
            .Select(characterLevel => new
            {
                characterLevel.Id,
                characterLevel.Character.Rarity,
            })
            .ToListAsync())
            .AsEnumerable()
            .Select(characterLevel => (characterLevel.Id, characterLevel.Rarity))
            .ToList(); 
    }
}
