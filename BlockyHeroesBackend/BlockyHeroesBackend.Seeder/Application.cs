using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Entities.Item;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Repositories.Query;
using System.Text.Json;

namespace BlockyHeroesBackend.Seeder;

public class Application
{
    private readonly IEquipCommandRepository _equipCommandRepository;
    private readonly IEquipQueryRepository _equipQueryRepository;
    private readonly IItemCommandRepository _itemCommandRepository;
    private readonly IItemQueryRepository _itemQueryRepository;
    private readonly IUserEquipmentCommandRepository _userEquipmentCommandRepository;
    private readonly IUserItemCommandRepository _userItemCommandRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSecurityService _userSecurityService;

    private List<User> _users = new List<User>();

    public Application(
        IEquipCommandRepository equipCommandRepository,
        IEquipQueryRepository equipQueryRepository,
        IItemCommandRepository itemCommandRepository,
        IItemQueryRepository itemQueryRepository,
        IUserEquipmentCommandRepository userEquipmentCommandRepository,
        IUserItemCommandRepository userItemCommandRepository,
        IUserCommandRepository userCommandRepository,
        IUnitOfWork unitOfWork, 
        IUserSecurityService userSecurityService)
    {
        _equipCommandRepository = equipCommandRepository;
        _equipQueryRepository = equipQueryRepository;
        _itemCommandRepository = itemCommandRepository;
        _itemQueryRepository = itemQueryRepository;

        _userEquipmentCommandRepository = userEquipmentCommandRepository;
        _userItemCommandRepository = userItemCommandRepository;

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

        Console.WriteLine("Creating random equipment to users");
        await GenerateRandomUserEquipment();

        Console.WriteLine("Creating random items to users");
        await GenerateRandomUserItems();

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

                UserEquipment userEquipment = new UserEquipment()
                {
                    Id = UserEquipmentId.CreateEquipmentId(),
                    EquipLevelId = equipLevel.Id,
                    EquipLevel = equipLevel,
                    UserId = user.Id,
                    Owner = user,
                    Quantity = random.Next(1, 40)
                };

                await _userEquipmentCommandRepository.InsertAsync(userEquipment);
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
}
