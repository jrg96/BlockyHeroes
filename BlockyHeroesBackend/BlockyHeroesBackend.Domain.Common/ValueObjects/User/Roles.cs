namespace BlockyHeroesBackend.Domain.Common.ValueObjects.User;

public record Roles(int Id, string Name)
{
    public static Roles User { get; } = new Roles(1, "User");
    public static Roles Admin { get; } = new Roles(2, "Admin");

    public static Roles GetRole(int id)
    {
        if (Roles.Admin.Id == id)
        {
            return Roles.Admin;
        }

        return Roles.User;
    }
}
