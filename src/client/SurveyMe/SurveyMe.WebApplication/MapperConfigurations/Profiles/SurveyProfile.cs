using AutoMapper;
using SurveyMe.DomainModels.Response;
using SurveyMe.WebApplication.Models.ViewModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyWithLinksResponseModel, SurveyWithLinksViewModel>();
    }
}