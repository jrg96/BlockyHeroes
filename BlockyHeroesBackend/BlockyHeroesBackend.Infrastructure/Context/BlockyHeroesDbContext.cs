using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Infrastructure.Context.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Context;

public class BlockyHeroesDbContext : DbContext, IBlockyHeroesDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Equip> Equips { get; set; }
    public DbSet<EquipLevel> EquipLevels { get; set; }
    public DbSet<UserEquipment> UserEquipments { get; set; }

    public BlockyHeroesDbContext(DbContextOptions<BlockyHeroesDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlockyHeroesDbContext).Assembly);
    }
}
