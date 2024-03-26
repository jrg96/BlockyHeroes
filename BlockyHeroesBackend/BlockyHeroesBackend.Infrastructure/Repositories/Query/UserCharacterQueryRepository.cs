using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.UserCharacter;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class UserCharacterQueryRepository : GenericQueryRepository<UserCharacter>, IUserCharacterQueryRepository
{
    public UserCharacterQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<UserCharacter?> GetById(UserCharacterId userCharacterId)
    {
        return await Find(new GetByUserCharacterIdSpecification(userCharacterId))
            .FirstOrDefaultAsync();
    }
}
