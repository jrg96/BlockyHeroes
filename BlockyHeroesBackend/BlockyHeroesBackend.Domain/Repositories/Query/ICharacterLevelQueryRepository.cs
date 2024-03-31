using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Entities.Character;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface ICharacterLevelQueryRepository : IGenericQueryRepository<CharacterLevel>
{
    Task<IEnumerable<(CharacterLevelId, ItemRarity)>> GetByCharacterLevelAndGachaTypeAsync(int level, GachaType gachaType);
}
