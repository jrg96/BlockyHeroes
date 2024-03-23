using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public abstract class GenericCommandRepository<T> : IGenericCommandRepository<T> where T : class
{
    private readonly BlockyHeroesDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected GenericCommandRepository(BlockyHeroesDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task DeleteAsync(T Entity)
    {
        _dbSet.Remove(Entity);
    }

    public async Task InsertAsync(T Entity)
    {
        await _dbSet.AddAsync(Entity);
    }

    public async Task UpdateAsync(T Entity)
    {
        _dbSet.Entry(Entity).State = EntityState.Modified;
    }
}
