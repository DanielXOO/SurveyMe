using MongoDB.Bson;
using Persons.Models.Persons;

namespace Persons.Services.Abstracts;

public interface IPersonalityService
{
    Task<Personality> GetPersonalityAsync(ObjectId id);
    
    Task AddPersonalityAsync(Personality personality);

    Task EditPersonalityAsync(Personality personality);

    Task DeletePersonality(Personality personality);
}