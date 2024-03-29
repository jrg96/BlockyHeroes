using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Entities.Character;

namespace BlockyHeroesBackend.Domain.Entities.Banner;

public class GachaBannerCharacter
{
    public GachaBannerCharacterId Id { get; set; }
    public float RateUp { get; set; }

    // Foreign key properties
    public GachaBannerId GachaBannerId { get; set; }
    public GachaBanner GachaBanner { get; set; }

    // In a gacha banner we will leave the possibility if developer wants, being able to pull characters higher than level 1
    public CharacterLevelId CharacterLevelId { get; set; }
    public CharacterLevel CharacterLevel { get; set; }

}
