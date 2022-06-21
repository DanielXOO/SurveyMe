using AutoMapper;
using Surveys.Api.Models.Request.Surveys;
using Surveys.Api.Models.Response.Surveys;
using Surveys.Models.Surveys;

namespace Surveys.Api.MapperConfiguration.Profiles;

public sealed class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyResponseModel, Survey>()
            .ReverseMap();
        
        CreateMap<SurveyAddRequestModel, Survey>()
            .ReverseMap();
        
        CreateMap<SurveyEditRequestModel, Survey>()
            .ReverseMap();
    }
}