namespace BlockyHeroesBackend.Domain.Common.ValueObjects.User;

public struct Roles(int Id, string Name)
{
    public static Roles User { get; } = new Roles(1, "User");
    public static Roles Admin { get; } = new Roles(2, "Admin");
}
