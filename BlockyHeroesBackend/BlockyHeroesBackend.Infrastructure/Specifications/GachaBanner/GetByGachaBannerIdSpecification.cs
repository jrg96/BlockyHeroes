using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

namespace BlockyHeroesBackend.Infrastructure.Specifications.GachaBanner;

public class GetByGachaBannerIdSpecification : Specification<Domain.Entities.Banner.GachaBanner>
{
    public GetByGachaBannerIdSpecification(GachaBannerId gachaBannerId)
    {
        Query
            .Include(gachaBanner => gachaBanner.DropRates)
            .Include(gachaBanner => gachaBanner.GachaBannerCurrencies)
                .ThenInclude(item => item.Item)
            .Include(gachaBanner => gachaBanner.GachaBannerCharacters)
                .ThenInclude(character => character.CharacterLevel)
                .ThenInclude(characterLevel => characterLevel.Character)
            .Where(gachaBanner => gachaBanner.Id == gachaBannerId);
    }
}
