using BlockyHeroesBackend.Domain.Entities.Item;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class ItemQueryRepository : GenericQueryRepository<Item>, IItemQueryRepository
{
    public ItemQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
