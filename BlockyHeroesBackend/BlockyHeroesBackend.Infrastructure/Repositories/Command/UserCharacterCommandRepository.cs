using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class UserCharacterCommandRepository : GenericCommandRepository<UserCharacter>, IUserCharacterCommandRepository
{
    public UserCharacterCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
