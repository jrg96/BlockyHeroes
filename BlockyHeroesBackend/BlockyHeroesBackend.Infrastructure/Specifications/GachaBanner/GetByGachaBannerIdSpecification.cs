using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

namespace BlockyHeroesBackend.Infrastructure.Specifications.GachaBanner;

public class GetByGachaBannerIdSpecification : Specification<Domain.Entities.Banner.GachaBanner>
{
    public GetByGachaBannerIdSpecification(GachaBannerId gachaBannerId)
    {
        Query
            .Include(gachaBanner => gachaBanner.GachaBannerCurrencies)
                .ThenInclude(item => item.Item)
            .Where(gachaBanner => gachaBanner.Id == gachaBannerId);
    }
}
