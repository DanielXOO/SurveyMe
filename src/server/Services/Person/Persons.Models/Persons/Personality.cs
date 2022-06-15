using MongoDB.Bson.Serialization.Attributes;
using Persons.Models.Common;

namespace Persons.Models.Persons;

public class Personality : BaseObject
{
    [BsonIgnoreIfNull]
    public string? FirstName { get; set; }
    
    [BsonIgnoreIfNull]
    public string? SecondName { get; set; }
    
    [BsonIgnoreIfNull]
    public int? Age { get; set; }

    [BsonIgnoreIfNull]
    public Gender? Gender { get; set; }

    public Guid UserId { get; set; }
}