namespace BlockyHeroesBackend.Presentation.Common;

public class TaskResult
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}

public class TaskResult<T> where T : class
{
    public T Data { get; set; }
}
