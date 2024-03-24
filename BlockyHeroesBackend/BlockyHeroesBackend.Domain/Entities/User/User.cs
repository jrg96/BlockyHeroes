using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Domain.Entities.User;

public class User
{
    public UserId Id { get; set; }
    public Roles Role { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public byte[] Salt { get; set; }

    // Foreign Key Relationships
    public ICollection<UserEquipment> UserEquipment { get; set; }
}
