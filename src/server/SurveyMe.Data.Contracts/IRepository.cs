using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyMe.Data.Contracts;

public interface IRepository<T>
{
    Task CreateAsync(T item);

    Task<T> FindByIdAsync(params object[] keyValues);

    Task<IReadOnlyCollection<T>> GetAllAsync();

    Task UpdateAsync(T item);

    Task DeleteAsync(T item);
}