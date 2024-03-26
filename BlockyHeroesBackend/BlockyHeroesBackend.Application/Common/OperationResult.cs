using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Application.Common;

public class OperationResult
{
    public bool Success { get; set; }
    public IEnumerable<Error> Errors { get; set; }

    public static OperationResult GenericInvalidOperation 
    { 
        get 
        { 
            return new OperationResult()
            {
                Success = false,
                Errors = new List<Error>() { new Error(-1, "Invalid Operation") }
            }; 
        } 
    }

    public static OperationResult GenericSuccess
    {
        get 
        {
            return new OperationResult()
            {
                Success = true
            };
        }
    }
}

public class OperationResult<T> : OperationResult where T : class
{
    public T? Data { get; set; }

    public static new OperationResult<T> GenericInvalidOperation
    {
        get
        {
            return new OperationResult<T>()
            {
                Success = false,
                Errors = new List<Error>() { new Error(-1, "Invalid Operation") },
                Data = null
            };
        }
    }

    public static new OperationResult<T> GenericSuccess
    {
        get
        {
            return new OperationResult<T>()
            {
                Success = true
            };
        }
    }
}
