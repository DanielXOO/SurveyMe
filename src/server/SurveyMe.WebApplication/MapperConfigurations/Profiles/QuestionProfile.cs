using AutoMapper;
using SurveyMe.Foundation.Models;
using SurveyMe.DomainModels.Questions;
using SurveyMe.WebApplication.Models.Requests.Questions;
using SurveyMe.WebApplication.Models.Responses.Questions;
using SurveyMe.WebApplication.Models.Responses.Statistics;

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