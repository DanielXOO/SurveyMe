using AutoMapper;
using SurveyMe.Foundation.Models;
using SurveyMe.DomainModels;
using SurveyMe.WebApplication.Models.RequestModels.QuestionModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionOptionRequestModel, QuestionOption>()
            .ReverseMap();

        CreateMap<QuestionRequestModel, Question>()
            .ReverseMap();
        
        CreateMap<QuestionOptionResponseModel, QuestionOption>()
            .ReverseMap();

        CreateMap<QuestionResponseModel, Question>()
            .ReverseMap();
        
        CreateMap<QuestionAnswersStatistic, QuestionAnswersStatisticResponseModel>();
    }
}