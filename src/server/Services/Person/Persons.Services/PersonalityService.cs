using MongoDB.Bson;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;
using Persons.Services.Abstracts;
using SurveyMe.Common.Exceptions;

namespace Persons.Services;

public sealed class PersonalityService : IPersonalityService
{
    private readonly IPersonalityRepository _repository;
    
    
    public PersonalityService(IPersonalityRepository repository)
    {
        _repository = repository;
    }

    
    public async Task<Personality> GetPersonalityAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        var personality = await _repository.GetByIdAsync(objectId);

        if (personality == null)
        {
            throw new BadRequestException("User do not exists");
        }
        
        return personality;
    }

    public async Task AddPersonalityAsync(Personality personality)
    {
        var isExist = await _repository.IsPersonalityExistAsync(personality.Id);
        
        if (isExist)
        {
            throw new NotFoundException("User personality already exists");
        }
        
        await _repository.CreateAsync(personality);
    }

    public async Task EditPersonalityAsync(Personality personality)
    {
        var isExist = await _repository.IsPersonalityExistAsync(personality.Id);
        
        if (!isExist)
        {
            throw new NotFoundException("User do not found");
        }
        
        await _repository.UpdateAsync(personality);
    }

    public async Task DeletePersonality(string id)
    {
        var objectId = ObjectId.Parse(id);
        
        var isExist = await _repository.IsPersonalityExistAsync(objectId);
        
        if (!isExist)
        {
            throw new NotFoundException("User do not found");
        }
        
        await _repository.DeleteAsync(objectId);
    }
}