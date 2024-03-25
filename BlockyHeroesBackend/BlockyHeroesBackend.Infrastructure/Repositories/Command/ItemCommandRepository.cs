using BlockyHeroesBackend.Domain.Entities.Item;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class ItemCommandRepository : GenericCommandRepository<Item>, IItemCommandRepository
{
    public ItemCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
