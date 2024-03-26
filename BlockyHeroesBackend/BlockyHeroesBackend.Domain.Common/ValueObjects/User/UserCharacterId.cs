namespace BlockyHeroesBackend.Domain.Common.ValueObjects.User;

public readonly record struct UserCharacterId(Guid Id)
{
    public static UserCharacterId Empty => new UserCharacterId(Guid.Empty);
    public static UserCharacterId CreateUserCharacterId()
    {
        return new UserCharacterId(Guid.NewGuid());
    }
}
