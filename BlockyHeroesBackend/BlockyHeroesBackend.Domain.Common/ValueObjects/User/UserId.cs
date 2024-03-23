namespace BlockyHeroesBackend.Domain.Common.ValueObjects.User;

public readonly record struct UserId(Guid value)
{
    public static UserId Empty => new UserId(Guid.Empty);
    public static UserId CreateUserId()
    {
        return new UserId(Guid.NewGuid());
    }
}