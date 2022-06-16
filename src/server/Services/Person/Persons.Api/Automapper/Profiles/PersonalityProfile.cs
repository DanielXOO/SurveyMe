using AutoMapper;
using MongoDB.Bson;
using Person.Api.Models.Request.Personality;
using Person.Api.Models.Response.Personality;
using Persons.Models.Persons;

namespace Person.Api.Automapper.Profiles;

public class PersonalityProfile : Profile
{
    public PersonalityProfile()
    {
        CreateMap<PersonalityCreateRequestModels, Personality>();
        
        CreateMap<PersonalityEditRequestModels, Personality>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.Parse(src.Id)));
        
        CreateMap<Personality, PersonalityResponseModel>();
    }
}