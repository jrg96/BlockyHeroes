namespace BlockyHeroesBackend.Presentation.RequestResponse.UserEquipment;

public class UpgradeUserEquipmentRequest
{
    public Guid Equipment { get; set; }
    public int Levels { get; set; }
}
