using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.User;

public class UserItemEntityConfiguration : IEntityTypeConfiguration<UserItem>
{
    public void Configure(EntityTypeBuilder<UserItem> builder)
    {
        builder
            .HasKey(userItem => userItem.Id);

        // Foreign Key configurations
        builder
            .HasOne(userItem => userItem.Item)
            .WithMany(item => item.UserItems)
            .HasForeignKey(userItem => userItem.ItemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(userItem => userItem.User)
            .WithMany(user => user.UserItems)
            .HasForeignKey(userItem => userItem.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Conversions
        builder
            .Property(userItem => userItem.Id)
            .HasConversion(
                id => id.Id,
                value => new UserItemId(value));

        builder
            .Property(userItem => userItem.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value));

        builder
            .Property(userItem => userItem.ItemId)
            .HasConversion(
                id => id.Id,
                value => new ItemId(value));
    }
}
