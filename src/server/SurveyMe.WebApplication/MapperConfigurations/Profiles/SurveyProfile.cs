using AutoMapper;
using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Surveys;
using SurveyMe.Foundation.Models.Statistics;
using SurveyMe.WebApplication.Models.Requests.Surveys;
using SurveyMe.WebApplication.Models.Responses.Statistics;
using SurveyMe.WebApplication.Models.Responses.Surveys;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyResponseModel, Survey>()
            .ReverseMap();
        
        CreateMap<SurveyRequestModel, Survey>()
            .ReverseMap();

        CreateMap<SurveyAnswerRequestModel, SurveyAnswer>();
        
        CreateMap<SurveyAnswersStatistic, SurveyAnswersStatisticResponseModel>();
    }
}