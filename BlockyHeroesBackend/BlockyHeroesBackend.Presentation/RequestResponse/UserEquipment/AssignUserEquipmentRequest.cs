namespace BlockyHeroesBackend.Presentation.RequestResponse.UserEquipment;

public class AssignUserEquipmentRequest
{
    public Guid UserCharacterId { get; set; }
    public Guid UserEquipmentId { get; set; }
    public int SlotToEquip { get; set; }
}
