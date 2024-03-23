using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(40);

        builder
            .HasIndex(user => user.Email)
            .IsUnique();

        builder
            .Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(user => user.Salt)
            .IsRequired()
            .HasMaxLength(128);


        // Custom Type conversions
        builder
            .Property(user => user.Id)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value));

        builder
            .Property(user => user.Role)
            .HasConversion(
                role => role.Id,
                value => Roles.GetRole(value));
    }
}
