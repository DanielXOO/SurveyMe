using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Persons.Models.Common;

public abstract class BaseObject
{
    [BsonId]
    public ObjectId Id { get; set; }
}