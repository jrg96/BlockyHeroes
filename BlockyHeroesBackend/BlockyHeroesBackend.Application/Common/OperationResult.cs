using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Application.Common;

public class OperationResult
{
    public bool Success { get; set; }
    public IEnumerable<Error> Errors { get; set; }
}

public class OperationResult<T> : OperationResult where T : class
{
    public T Data { get; set; }
}
