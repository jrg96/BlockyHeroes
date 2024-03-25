using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using System.Text.Json;

namespace BlockyHeroesBackend.Seeder;

public class Application
{
    private readonly IEquipCommandRepository _equipCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public Application(
        IEquipCommandRepository equipCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _equipCommandRepository = equipCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task RunSeeder()
    {
        await LoadEquips();
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
}
