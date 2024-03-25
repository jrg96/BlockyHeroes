using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class UserItemQueryRepository : GenericQueryRepository<UserItem>, IUserItemQueryRepository
{
    public UserItemQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
