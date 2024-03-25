namespace BlockyHeroesBackend.Domain.Common.ValueObjects.User;

public readonly record struct UserItemId(Guid Id)
{
    public static UserItemId Empty => new UserItemId(Guid.Empty);

    public static UserItemId CreateUserItemId()
    {
        return new UserItemId(Guid.NewGuid());
    }
}
