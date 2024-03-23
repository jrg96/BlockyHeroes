using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlockyHeroesDbContext _context;

    public UnitOfWork(BlockyHeroesDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
