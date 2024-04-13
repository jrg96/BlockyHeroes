using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Entities.Item;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;
using System.Text.Json;

namespace BlockyHeroesBackend.Seeder;

public class Application
{
    private readonly IGachaBannerCommandRepository _gachaBannerCommandRepository;
    private readonly IEquipCommandRepository _equipCommandRepository;
    private readonly IEquipQueryRepository _equipQueryRepository;
    private readonly IItemCommandRepository _itemCommandRepository;
    private readonly IItemQueryRepository _itemQueryRepository;
    private readonly ICharacterCommandRepository _characterCommandRepository;
    private readonly ICharacterQueryRepository _characterQueryRepository;
    private readonly IUserEquipmentCommandRepository _userEquipmentCommandRepository;
    private readonly IUserItemCommandRepository _userItemCommandRepository;
    private readonly IUserCharacterCommandRepository _userCharacterCommandRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSecurityService _userSecurityService;

    private List<User> _users = new List<User>();

    public Application(
        IGachaBannerCommandRepository gachaBannerCommandRepository,
        IEquipCommandRepository equipCommandRepository,
        IEquipQueryRepository equipQueryRepository,
        IItemCommandRepository itemCommandRepository,
        IItemQueryRepository itemQueryRepository,
        ICharacterCommandRepository characterCommandRepository,
        ICharacterQueryRepository characterQueryRepository,
        IUserEquipmentCommandRepository userEquipmentCommandRepository,
        IUserItemCommandRepository userItemCommandRepository,
        IUserCharacterCommandRepository userCharacterCommandRepository,
        IUserCommandRepository userCommandRepository,
        IUnitOfWork unitOfWork, 
        IUserSecurityService userSecurityService)
    {
        _gachaBannerCommandRepository = gachaBannerCommandRepository;

        _equipCommandRepository = equipCommandRepository;
        _equipQueryRepository = equipQueryRepository;
        _itemCommandRepository = itemCommandRepository;
        _itemQueryRepository = itemQueryRepository;
        _characterCommandRepository = characterCommandRepository;
        _characterQueryRepository = characterQueryRepository;

        _userEquipmentCommandRepository = userEquipmentCommandRepository;
        _userItemCommandRepository = userItemCommandRepository;
        _userCharacterCommandRepository = userCharacterCommandRepository;
        

        _userCommandRepository = userCommandRepository;
        _unitOfWork = unitOfWork;
        _userSecurityService = userSecurityService;
        
        
    }

    public async Task RunSeeder()
    {
        Console.WriteLine("Building user list");
        await LoadUsers();

        Console.WriteLine("Building Equipment List");
        await LoadEquips();

        Console.WriteLine("Building Item List");
        await LoadItems();

        Console.WriteLine("Building Character List");
        await LoadCharacters();

        Console.WriteLine("Creating random equipment to users");
        await GenerateRandomUserEquipment();

        Console.WriteLine("Creating random items to users");
        await GenerateRandomUserItems();

        Console.WriteLine("Creating random characters to users");
        await GenerateRandomUserCharacters();

        Console.WriteLine("Creating gacha banners");
        await GenerateGachaBanners();

        Console.WriteLine("Sample data loaded successfully!");
    }

    private async Task LoadUsers()
    {
        // For this seeder default password will be ABBBCCDDSSSD
        string defaultPassword = "ABBBCCDDSSSD";
        (byte[] salt, string hash) = _userSecurityService.HashPassword(defaultPassword);

        for (int i=0; i < 5; i++)
        {
            UserId id = UserId.CreateUserId();
            var user = new User()
            {
                Id = id,
                Name = $"Player{i.ToString()}",
                Email = $"{id.ToString()}@blockyheroes.com",
                Coins = 100000,
                Password = hash,
                Salt = salt,
                Role = Roles.User,
                Stamina = 80,
                MaxStamina = 80
            };

            await _userCommandRepository.InsertAsync(user);
            _users.Add(user);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task LoadEquips()
    {
        string data = File.ReadAllText("Samples/equipment.json");
        var equips = JsonSerializer.Deserialize<List<Equip>>(data);
        
        foreach(var eq in equips)
        {
            eq.Id = EquipId.CreateEquipId();
            foreach (var equipLevel in eq.EquipmentEvolutions)
            {
                equipLevel.Id = EquipLevelId.CreateEquipId();
            }

            await _equipCommandRepository.InsertAsync(eq);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task LoadItems()
    {
        string data = File.ReadAllText("Samples/items.json");
        var items = JsonSerializer.Deserialize<List<Item>>(data);

        foreach(var item in items)
        {
            item.Id = ItemId.CreateItemId();
            await _itemCommandRepository.InsertAsync(item);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task LoadCharacters()
    {
        string data = File.ReadAllText("Samples/characters.json");

        var characters = JsonSerializer.Deserialize<List<Character>>(data);
        var items = await _itemQueryRepository.GetAllAsync();

        foreach(var character in characters)
        {
            character.Id = CharacterId.CreateCharacterId();

            // Fill Ids of CharacterLevel
            character.CharacterLevels.AsParallel()
                .ForAll(charLevel =>
                    charLevel.Id = CharacterLevelId.CreateCharacterLevelId());

            // Fill Ids of character requirement
            character.CharacterLevels
                .SelectMany(charLevel => charLevel.CharacterLevelRequirements)
                .AsParallel()
                .ForAll(requirement =>
                {
                    requirement.Id = CharacterLevelRequirementId.CreateCharacterLevelRequirementId();
                    requirement.Item =
                    items.Where(item => item.Name == requirement.Item.Name)
                    .FirstOrDefault();
                    requirement.ItemId = requirement.Item.Id;
                });

            await _characterCommandRepository.InsertAsync(character);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task GenerateRandomUserEquipment()
    {
        IEnumerable<Equip> availableEquipment = await _equipQueryRepository.GetAllAsync();

        foreach(var user in _users)
        {
            foreach(var equip in availableEquipment)
            {
                Random random = new Random();

                // Generate a couple of elements for the first level
                EquipLevel? equipLevel = equip.EquipmentEvolutions
                    .Where(eql => eql.Level == 1)
                    .FirstOrDefault();

                int max = random.Next(1, 15);

                for (int i = 0; i < max; i++)
                {
                    UserEquipment userEquipment = new UserEquipment()
                    {
                        Id = UserEquipmentId.CreateEquipmentId(),
                        EquipLevelId = equipLevel.Id,
                        EquipLevel = equipLevel,
                        UserId = user.Id,
                        Owner = user
                    };

                    await _userEquipmentCommandRepository.InsertAsync(userEquipment);
                }
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task GenerateRandomUserItems()
    {
        var items = await _itemQueryRepository.GetAllAsync();

        foreach(var user in _users)
        {
            foreach(var item in items)
            {
                Random random = new Random();

                UserItem userItem = new UserItem()
                {
                    Id = UserItemId.CreateUserItemId(),
                    Quantity = random.Next(1, 500),
                    UserId = user.Id,
                    User = user,
                    ItemId = item.Id,
                    Item = item,
                };

                await _userItemCommandRepository.InsertAsync(userItem);
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task GenerateRandomUserCharacters()
    {
        var characters = await _characterQueryRepository.GetAllAsync();

        foreach(var user in _users)
        {
            foreach(var character in characters)
            {
                var firstLevel = character.CharacterLevels
                    .Where(charLevel => charLevel.Level == 1)
                    .FirstOrDefault();

                UserCharacter userCharacter = new UserCharacter()
                {
                    Id = UserCharacterId.CreateUserCharacterId(),
                    CharacterLevel = firstLevel,
                    CharacterLevelId = firstLevel.Id,
                    Owner = user,
                    UserId = user.Id,
                    UserEquipmentIdSlot1 = null,
                    UserEquipmentIdSlot2 = null,
                };

                await _userCharacterCommandRepository.InsertAsync(userCharacter);
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task GenerateGachaBanners()
    {
        var items = await _itemQueryRepository.GetAllAsync();
        string data = File.ReadAllText("Samples/gachabanner.json");
        var gachaBanners = JsonSerializer.Deserialize<List<GachaBanner>>(data);

        foreach (var banner in gachaBanners)
        {
            foreach (var rate in banner.DropRates)
            {
                rate.Id = BannerDropRateId.CreateBannerDropRateId();
            }

            foreach (var gachaCurrency in banner.GachaBannerCurrencies)
            {
                gachaCurrency.Id = GachaBannerCurrencyId.CreateGachaBannerCurrencyId();
                gachaCurrency.Item = items.First(item => item.Name.ToLower() == gachaCurrency.Item.Name.ToLower());
                gachaCurrency.ItemId = gachaCurrency.Item.Id;
            }

            banner.Id = GachaBannerId.CreateBannerId();

            await _gachaBannerCommandRepository.InsertAsync(banner);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
