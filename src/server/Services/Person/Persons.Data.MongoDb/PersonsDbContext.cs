using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persons.Models.Configurations;

namespace Persons.Data;

public sealed class PersonsDbContext
{
    private readonly IMongoDatabase _db;


    public PersonsDbContext(IOptions<DbConfiguration> configuration)
    {
        var client = new MongoClient(configuration.Value.Connection);
        
        _db = client.GetDatabase(configuration.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        var collection = _db.GetCollection<T>(name);

        return collection;
    }
}