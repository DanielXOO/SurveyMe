using MongoDB.Bson;
using Persons.Models.Common;

namespace Persons.Data.Core.Abstracts;

public interface IRepository<T> where T : BaseObject
{
    Task CreateAsync(T data);

    Task UpdateAsync(T data);
    
    Task<T> GetByIdAsync(ObjectId id);

    Task DeleteAsync(T data);
}