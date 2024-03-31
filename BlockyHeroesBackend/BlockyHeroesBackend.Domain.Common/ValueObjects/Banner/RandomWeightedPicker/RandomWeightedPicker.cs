using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner.RandomWeightedPicker;

public class RandomWeightedPicker<T> where T : IWeighted
{
    private readonly IEnumerable<T> _items;
    private readonly Random _random = new Random();
    private readonly Dictionary<ItemRarity, float> _rarityProbabilities;

    public RandomWeightedPicker(IEnumerable<T> items, Dictionary<ItemRarity, float> rarityProbabilities)
    {
        _items = items;
        _rarityProbabilities = rarityProbabilities;
    }

    public T PickItem()
    {
        int totalWeight = 0;
        double selectedRariryProbability = _random.NextDouble();
        double accumulatedProbability = 0.0d;

        ItemRarity selectedRarity = ItemRarity.Rare;
        IEnumerable<T> selectedItems;

        // Step 1: Select the rarity for this pick
        for (int i = 0; i < _rarityProbabilities.Count - 1; i++)
        {
            accumulatedProbability += _rarityProbabilities.ElementAt(i).Value;
            if (selectedRariryProbability <= accumulatedProbability)
            {
                selectedRarity = _rarityProbabilities.ElementAt(i).Key;
                break;
            }
        }

        selectedItems = _items.Where(item => item.ItemRarity == selectedRarity);
        totalWeight = selectedItems.Sum(item => item.Weight);

        // Step 2: select a random pick
        int selectedValue = _random.Next(totalWeight);
        return _items.First(i => (selectedValue -= i.Weight) < 0);
    }

    public IEnumerable<T> PickItems(int amount)
    {
        return Enumerable.Repeat(0, amount)
            .Select(i => PickItem())
            .ToList();
    }
}
