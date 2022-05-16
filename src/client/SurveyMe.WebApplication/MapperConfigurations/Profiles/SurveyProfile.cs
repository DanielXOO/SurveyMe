using AutoMapper;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
using SurveyMe.WebApplication.Models.ViewModels;

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