namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

public class Error
{
    public int Code { get; set; }
    public string Message { get; set; }

    public static Error None => new Error { Code = 0, Message = string.Empty };
}
