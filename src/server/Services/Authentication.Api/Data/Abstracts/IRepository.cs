namespace Authentication.Api.Data.Abstracts;

public interface IRepository<T>
{
    Task CreateAsync(T data);

    Task<IReadOnlyCollection<T>> ReadAllAsync();
    
    Task UpdateAsync(T data);

    Task DeleteAsync(T data);
}