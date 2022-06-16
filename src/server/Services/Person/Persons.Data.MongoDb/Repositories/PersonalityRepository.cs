using MongoDB.Driver;
using Persons.Data.Core;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories;

public sealed class PersonalityRepository : Repository<Personality>, IPersonalityRepository
{
    public PersonalityRepository(PersonsDbContext dbContext) : base(dbContext) { }


    public async Task<bool> IsUserPersonalityExists(Guid userId)
    {
        var isExist = await Collection
            .Find(personality => personality.UserId == userId)
            .AnyAsync();

        return isExist;
    }
}