using MongoDB.Bson;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;
using Persons.Services.Abstracts;

namespace Persons.Services;

public sealed class PersonalityService : IPersonalityService
{
    private readonly IPersonalityRepository _repository;
    
    
    public PersonalityService(IPersonalityRepository repository)
    {
        _repository = repository;
    }

    
    public async Task<Personality> GetPersonalityAsync(ObjectId id)
    {
        var personality = await _repository.GetByIdAsync(id);

        return personality;
    }

    public async Task AddPersonalityAsync(Personality personality)
    {
        await _repository.CreateAsync(personality);
    }

    public async Task EditPersonalityAsync(Personality personality)
    {
        await _repository.UpdateAsync(personality);
    }

    public async Task DeletePersonality(Personality personality)
    {
        await _repository.DeleteAsync(personality);
    }
}