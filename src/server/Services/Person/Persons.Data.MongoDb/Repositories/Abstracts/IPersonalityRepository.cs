using MongoDB.Bson;
using Persons.Data.Core.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories.Abstracts;

public interface IPersonalityRepository : IRepository<Personality>
{
    Task<bool> IsPersonalityExistAsync(ObjectId id);
}