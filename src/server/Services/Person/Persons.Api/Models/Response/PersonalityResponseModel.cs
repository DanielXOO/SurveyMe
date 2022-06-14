using MongoDB.Bson;
using Persons.Models.Persons;

namespace Person.Api.Models.Response;

public class PersonalityResponseModel
{
    public ObjectId Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? SecondName { get; set; }
    
    public int? Age { get; set; }

    public Gender? Gender { get; set; }

    public Guid UserId { get; set; }
}