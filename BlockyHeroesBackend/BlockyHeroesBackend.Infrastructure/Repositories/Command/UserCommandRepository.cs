using BlockyHeroesBackend.Domain.Entities;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class UserCommandRepository : GenericCommandRepository<User>, IUserCommandRepository
{
    public UserCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
