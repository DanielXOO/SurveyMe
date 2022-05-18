using AutoMapper;
using SurveyMe.DomainModels.Response;
using SurveyMe.WebApplication.Models.ViewModels.Pages;
using SurveyMe.WebApplication.Models.ViewModels.Surveys;
using SurveyMe.WebApplication.Models.ViewModels.Users;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class PageResponseProfile : Profile
{
    public PageResponseProfile()
    {
        CreateMap<PageResponseModel<UserWithSurveysCountResponseModel>,
            PageResponseViewModel<UserWithSurveysCountViewModel>>();

        CreateMap<PagedResultResponseModel<UserWithSurveysCountResponseModel>,
            PagedResultViewModel<UserWithSurveysCountViewModel>>();

        CreateMap<PageResponseModel<SurveyResponseModel>, PageResponseViewModel<SurveyWithLinksViewModel>>();
        
        CreateMap<PagedResultResponseModel<SurveyResponseModel>,
            PagedResultViewModel<SurveyWithLinksViewModel>>();
    }
}