using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.UserItem;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class UserItemQueryRepository : GenericQueryRepository<UserItem>, IUserItemQueryRepository
{
    public UserItemQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserItem>> GetByUserId(UserId userId)
    {
        return await Find(new GetByUserIdSpecification(userId))
            .ToListAsync();
    }
}
