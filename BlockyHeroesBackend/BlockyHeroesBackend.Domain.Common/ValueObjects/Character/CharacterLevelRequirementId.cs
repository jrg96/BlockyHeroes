namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Character;

public readonly record struct CharacterLevelRequirementId(Guid Id)
{
    public static CharacterLevelRequirementId Empty => new CharacterLevelRequirementId(Guid.Empty);
    public static CharacterLevelRequirementId CreateCharacterLevelRequirementId()
    {
        return new CharacterLevelRequirementId(Guid.NewGuid());
    }
}
