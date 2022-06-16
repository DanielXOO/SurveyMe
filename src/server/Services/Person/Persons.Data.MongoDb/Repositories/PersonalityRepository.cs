using MongoDB.Bson;
using MongoDB.Driver;
using Persons.Data.Core;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories;

public sealed class PersonalityRepository : Repository<Personality>, IPersonalityRepository
{
    public PersonalityRepository(PersonsDbContext dbContext) : base(dbContext) { }
    
    
    public async Task<bool> IsPersonalityExistAsync(ObjectId id)
    {
        var isExist = await Collection
            .Find(personality => personality.Id == id)
            .AnyAsync();

        return isExist;
    }
}