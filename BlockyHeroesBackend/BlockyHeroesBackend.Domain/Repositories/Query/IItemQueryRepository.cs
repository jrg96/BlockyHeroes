using BlockyHeroesBackend.Domain.Entities.Item;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface IItemQueryRepository : IGenericQueryRepository<Item>
{
    Task<IEnumerable<Item>> GetAllAsync();
}
