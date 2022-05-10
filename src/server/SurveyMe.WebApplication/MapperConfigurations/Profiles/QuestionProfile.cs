using AutoMapper;
using SurveyMe.Foundation.Models;
using SurveyMe.DomainModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionOptionResponseModel, QuestionOption>()
            .ReverseMap();

        CreateMap<QuestionResponseModel, Question>()
            .ReverseMap();
        
        CreateMap<QuestionAnswersStatistic, QuestionAnswersStatisticResponseModel>();
    }
}