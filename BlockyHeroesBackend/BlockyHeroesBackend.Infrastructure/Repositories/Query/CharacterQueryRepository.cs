using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.Character;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class CharacterQueryRepository : GenericQueryRepository<Character>, ICharacterQueryRepository
{
    public CharacterQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Character>> GetAllAsync()
    {
        return await Find(new GetAllSpecification())
            .ToListAsync();
    }
}
