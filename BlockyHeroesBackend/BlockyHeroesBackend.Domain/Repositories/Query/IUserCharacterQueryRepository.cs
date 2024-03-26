using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface IUserCharacterQueryRepository : IGenericQueryRepository<UserCharacter>
{
    Task<UserCharacter?> GetById(UserCharacterId userCharacterId);
}
