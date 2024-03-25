using BlockyHeroesBackend.Domain.Entities.Item;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.Item;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class ItemQueryRepository : GenericQueryRepository<Item>, IItemQueryRepository
{
    public ItemQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await Find(new GetAllSpecification())
            .ToListAsync();
    }
}
