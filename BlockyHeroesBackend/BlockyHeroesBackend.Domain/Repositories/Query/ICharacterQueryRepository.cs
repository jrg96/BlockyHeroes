using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Entities.Character;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface ICharacterQueryRepository : IGenericQueryRepository<Character>
{
    Task<Character?> GetByIdAsync(CharacterId characterId);
    Task<IEnumerable<Character>> GetAllAsync();
}
