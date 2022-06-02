using AutoMapper;
using SurveyMe.DomainModels.Request.Answers;
using SurveyMe.DomainModels.Request.Surveys;
using SurveyMe.DomainModels.Response;
using SurveyMe.DomainModels.Response.Answers;
using SurveyMe.DomainModels.Response.Surveys;
using SurveyMe.WebApplication.Models.ViewModels.Answers;
using SurveyMe.WebApplication.Models.ViewModels.Statistics;
using SurveyMe.WebApplication.Models.ViewModels.Surveys;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyResponseModel, SurveyWithLinksViewModel>();
        
        CreateMap<SurveyResponseModel, SurveyViewModel>();
        
        CreateMap<SurveyAddOrEditViewModel, SurveyRequestModel>().ReverseMap();
        
        CreateMap<SurveyAddOrEditViewModel, SurveyResponseModel>().ReverseMap();

        CreateMap<AnswerViewModel, SurveyAnswerRequestModel>();

        CreateMap<SurveyAnswersStatisticResponseModel, SurveyAnswersStatisticViewModel>();
    }
}