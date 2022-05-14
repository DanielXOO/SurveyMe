using AutoMapper;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
using SurveyMe.WebApplication.Models.ViewModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserWithSurveysCountResponseModel, UserWithSurveysCountViewModel>();
        
        CreateMap<UserDeleteOrEditResponseModel, UserDeleteOrEditViewModel>();
        
        CreateMap<UserDeleteOrEditViewModel, UserDeleteOrEditRequestModel>();
        
        CreateMap<UserLoginViewModel, UserLoginRequestModel>();
    }
}