using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Entities.Item;
using BlockyHeroesBackend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Context.Contracts;

public interface IBlockyHeroesDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserEquipment> UserEquipments { get; set; }
    public DbSet<UserItem> UserItems { get; set; }

    public DbSet<Equip> Equips { get; set; }
    public DbSet<EquipLevel> EquipLevels { get; set; }

    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterLevel> CharacterLevels { get; set; }
    public DbSet<CharacterLevelRequirement> CharacterLevelRequirements { get; set; }

    public DbSet<Item> Items { get; set; }
}
