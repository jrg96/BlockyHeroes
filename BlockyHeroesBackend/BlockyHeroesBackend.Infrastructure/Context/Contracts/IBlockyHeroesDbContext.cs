using BlockyHeroesBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Context.Contracts;

public interface IBlockyHeroesDbContext
{
    public DbSet<User> Users { get; set; }
}
