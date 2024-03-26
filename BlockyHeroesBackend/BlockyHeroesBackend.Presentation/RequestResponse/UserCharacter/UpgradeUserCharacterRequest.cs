namespace BlockyHeroesBackend.Presentation.RequestResponse.UserCharacter;

public class UpgradeUserCharacterRequest
{
    public Guid UserCharacterId { get; set; }
    public int LevelsToUpgrade { get; set; }
}
