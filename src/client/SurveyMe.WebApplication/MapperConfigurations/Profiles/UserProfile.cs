using AutoMapper;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.DomainModels.Response.Users;
using SurveyMe.WebApplication.Models.ViewModels.Users;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationViewModel, UserRegistrationRequestModel>();

        CreateMap<UserWithSurveysCountResponseModel, UserWithSurveysCountViewModel>();

        CreateMap<UserDeleteOrEditResponseModel, UserDeleteOrEditViewModel>();

        CreateMap<UserDeleteOrEditViewModel, UserDeleteOrEditRequestModel>();

        CreateMap<UserLoginViewModel, UserLoginRequestModel>();
    }
}