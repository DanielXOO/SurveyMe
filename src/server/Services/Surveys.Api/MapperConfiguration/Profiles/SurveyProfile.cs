using AutoMapper;
using Surveys.Api.Models.Request.Surveys;
using Surveys.Api.Models.Response.Surveys;
using Surveys.Api.Models.Surveys;

namespace Surveys.Api.MapperConfiguration.Profiles;

public sealed class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyResponseModel, Survey>()
            .ReverseMap();
        
        CreateMap<SurveyRequestModel, Survey>()
            .ReverseMap();
    }
}