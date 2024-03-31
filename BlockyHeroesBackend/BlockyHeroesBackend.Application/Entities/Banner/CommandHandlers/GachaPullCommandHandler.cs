using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.Banner.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner.RandomWeightedPicker;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Query;

namespace BlockyHeroesBackend.Application.Entities.Banner.CommandHandlers;

public class GachaPullCommandHandler : IOperationHandler<GachaPullCommand, IEnumerable<Domain.Entities.User.UserCharacter>>
{
    private readonly IGachaBannerQueryRepository _gachaBannerQueryRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly ICharacterLevelQueryRepository _characterLevelQueryRepository;

    public GachaPullCommandHandler(
        IGachaBannerQueryRepository gachaBannerQueryRepository, 
        IUserQueryRepository userQueryRepository,
        ICharacterLevelQueryRepository characterLevelQueryRepository)
    {
        _gachaBannerQueryRepository = gachaBannerQueryRepository;
        _userQueryRepository = userQueryRepository;
        _characterLevelQueryRepository = characterLevelQueryRepository;
    }

    public GachaType GachaType { get; private set; }

    public async Task<OperationResult<IEnumerable<Domain.Entities.User.UserCharacter>>> Handle(GachaPullCommand request, CancellationToken cancellationToken)
    {
        GachaBannerId gachaBannerId = new GachaBannerId(request.GachaBannerId);
        UserId userId = new UserId(request.UserId);

        // Step 1: Check if Banner is still active
        GachaBanner? bannerToPullFrom = await _gachaBannerQueryRepository.GetByIdAsync(gachaBannerId);
        if (!(bannerToPullFrom?.StartDate >= DateTime.Now && bannerToPullFrom?.EndDate <= DateTime.Now))
        {
            return OperationResult<IEnumerable<Domain.Entities.User.UserCharacter>>.GenericInvalidOperation;
        }

        // Step 2: Check if user has all available resources
        Domain.Entities.User.User? user = await _userQueryRepository.GetByIdAsync(userId);
        IEnumerable<UserItem>? userGachaResources = user?.UserItems?
            .Where(item => bannerToPullFrom.GachaBannerCurrencies.Any(bannerCurrency => bannerCurrency.ItemId == item.Item.Id))?
            .ToList();

        bool? hasEnoughResources = userGachaResources?
            .All(userItem => userItem.Quantity >= 
                bannerToPullFrom
                    .GachaBannerCurrencies
                    .FirstOrDefault(currency => currency.ItemId == userItem.ItemId)?
                    .QuantityPerPull * request.NumberOfPulls);
        
        if (!(hasEnoughResources.HasValue && hasEnoughResources.Value))
        {
            return OperationResult<IEnumerable<Domain.Entities.User.UserCharacter>>.GenericInvalidOperation;
        }

        // Step 3: After verifying banner and resource availability
        // Proceed to do gacha pull

        // Build consolidated list of possible characters
        // By default, permanent pool has a weight = 1 to appear
        IEnumerable<WeightedCharacter> selectionPool = WeightedCharacter
            .CreateWeightedCharacters(
                await _characterLevelQueryRepository.GetByCharacterLevelAndGachaTypeAsync(1, GachaType.PermanentPool), 
                1)
            .Union(bannerToPullFrom.GachaBannerCharacters
                .Select(exclusiveCharacter => 
                    WeightedCharacter.CreateWeightedCharacter(
                            exclusiveCharacter.CharacterLevelId,
                            exclusiveCharacter.CharacterLevel.Character.Rarity,
                            (int)exclusiveCharacter.RateUp)));

        Dictionary<ItemRarity, float> rarityDropRates = bannerToPullFrom
            .DropRates
            .ToList()
            .ToDictionary(dropRate => dropRate.Rarity, dropRate => dropRate.DropRate);

        RandomWeightedPicker<WeightedCharacter> picker = 
            new RandomWeightedPicker<WeightedCharacter>(
                    selectionPool, 
                    rarityDropRates);

        // Perform Gacha Pull
        IEnumerable<WeightedCharacter> pulls = picker.PickItems(request.NumberOfPulls);

        return OperationResult<IEnumerable<Domain.Entities.User.UserCharacter>>.GenericSuccess;
    }
}
