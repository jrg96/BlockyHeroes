using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public abstract class GenericQueryRepository<T> : IGenericQueryRepository<T> where T : class
{
    private readonly BlockyHeroesDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected GenericQueryRepository(BlockyHeroesDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    protected IQueryable<T> Find(ISpecification<T> specification)
    {
        return SpecificationEvaluator.Default.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }

    protected async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await Find(specification).CountAsync();
    }
}
