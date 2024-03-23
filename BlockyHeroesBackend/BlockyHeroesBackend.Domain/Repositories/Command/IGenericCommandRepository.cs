namespace BlockyHeroesBackend.Domain.Repositories.Command;

public interface IGenericCommandRepository<T> where T : class
{
    Task InsertAsync(T Entity);
    Task UpdateAsync(T Entity);
    Task DeleteAsync(T Entity);
}
