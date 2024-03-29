using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Entities.Item;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Infrastructure.Context.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Context;

public class BlockyHeroesDbContext : DbContext, IBlockyHeroesDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserEquipment> UserEquipments { get; set; }
    public DbSet<UserItem> UserItems { get; set; }
    public DbSet<UserCharacter> UserCharacters { get; set; }

    public DbSet<Equip> Equips { get; set; }
    public DbSet<EquipLevel> EquipLevels { get; set; }

    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterLevel> CharacterLevels { get; set; }
    public DbSet<CharacterLevelRequirement> CharacterLevelRequirements { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<GachaBanner> GachaBanners { get; set; }
    public DbSet<BannerDropRate> BannerDropRates { get; set; }

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
