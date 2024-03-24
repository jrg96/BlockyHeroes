namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

public class Error
{
    public Error()
    {
        
    }

    public Error(int code, string message)
    {
        Code = code; 
        Message = message;
    }

    public int Code { get; set; }
    public string Message { get; set; }

    public static Error None => new Error { Code = 0, Message = string.Empty };
}
