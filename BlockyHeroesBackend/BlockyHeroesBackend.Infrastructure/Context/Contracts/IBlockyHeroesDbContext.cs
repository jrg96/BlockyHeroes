using BlockyHeroesBackend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Context.Contracts;

public interface IBlockyHeroesDbContext
{
    public DbSet<User> Users { get; set; }
}
