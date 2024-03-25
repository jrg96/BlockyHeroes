namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Character;

public readonly record struct CharacterLevelId(Guid Id)
{
    public static CharacterLevelId Empty => new CharacterLevelId(Guid.Empty);
    public static CharacterLevelId CreateCharacterLevelId()
    {
        return new CharacterLevelId(Guid.NewGuid());
    }
}
