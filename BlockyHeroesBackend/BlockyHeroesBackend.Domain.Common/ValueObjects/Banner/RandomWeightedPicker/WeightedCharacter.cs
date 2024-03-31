using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner.RandomWeightedPicker;

public class WeightedCharacter : IWeighted
{
    public int Weight { get; set; }
    public CharacterLevelId CharacterLevelId { get; set; }
    public ItemRarity ItemRarity { get; set; }

    public static IEnumerable<WeightedCharacter> CreateWeightedCharacters(IEnumerable<(CharacterLevelId, ItemRarity)> characters, int defaultWeight)
    {
        return characters
            .Select(character =>
            {
                return new WeightedCharacter()
                {
                    CharacterLevelId = character.Item1,
                    ItemRarity = character.Item2,
                    Weight = defaultWeight
                };
            }).ToList();
    }

    public static WeightedCharacter CreateWeightedCharacter(CharacterLevelId characterLevelId, ItemRarity rarity, int weight)
    {
        return new WeightedCharacter()
        {
            CharacterLevelId = characterLevelId,
            ItemRarity = rarity,
            Weight = weight
        };
    }
}
