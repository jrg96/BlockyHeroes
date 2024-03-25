namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Character;

public readonly record struct CharacterId(Guid Id)
{
    public static CharacterId Empty => new CharacterId(Guid.Empty);

    public static CharacterId CreateCharacterId()
    {
        return new CharacterId(Guid.NewGuid());
    }
}
