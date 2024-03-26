using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class UserCharacterQueryRepository : GenericQueryRepository<UserCharacter>, IUserCharacterQueryRepository
{
    public UserCharacterQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
