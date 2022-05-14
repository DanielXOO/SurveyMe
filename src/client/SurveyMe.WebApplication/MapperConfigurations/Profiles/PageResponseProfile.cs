using AutoMapper;
using SurveyMe.DomainModels.Response;
using SurveyMe.WebApplication.Models.ViewModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class PageResponseProfile : Profile
{
    public PageResponseProfile()
    {
        CreateMap<PageResponseModel<UserWithSurveysCountResponseModel>,
            PageResponseViewModel<UserWithSurveysCountViewModel>>();

        CreateMap<PagedResultResponseModel<UserWithSurveysCountResponseModel>,
            PagedResultViewModel<UserWithSurveysCountViewModel>>();

        CreateMap<PageResponseModel<SurveyWithLinksResponseModel>, PageResponseViewModel<SurveyWithLinksViewModel>>();
        
        CreateMap<PagedResultResponseModel<SurveyWithLinksResponseModel>,
            PagedResultViewModel<SurveyWithLinksViewModel>>();
    }
}