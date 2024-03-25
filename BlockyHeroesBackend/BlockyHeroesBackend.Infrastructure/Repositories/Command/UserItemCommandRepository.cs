using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class UserItemCommandRepository : GenericCommandRepository<UserItem>, IUserItemCommandRepository
{
    public UserItemCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
