using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.User;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class UserQueryRepository : GenericQueryRepository<User>, IUserQueryRepository
{
    public UserQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByIdAsync(UserId userId)
    {
        return await Find(new GetUserByUserIdSpecification(userId))
            .FirstOrDefaultAsync();
    }
}
