using AutoMapper;
using SurveyMe.DomainModels.Users;
using SurveyMe.Foundation.Models.Users;
using SurveyMe.WebApplication.Models.Requests.Users;
using SurveyMe.WebApplication.Models.Responses.Users;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEditRequestModel, User>();
        
        CreateMap<UserEditResponseModel, User>().ReverseMap();
        
        
        CreateMap<UserRegistrationRequestModel, User>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.DisplayName,
                opt => opt.MapFrom(src => src.Name));
        
        CreateMap<UserWithSurveysCount, UserWithSurveysCountResponseModel>()
            .ForMember(dest => dest.RoleNames, 
                opt => opt.MapFrom(src => src.User.Roles.Select(role => role.Name)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.User.CreationTime))
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
            .ForMember(des => des.SurveysCount, opt => opt.MapFrom(src => src.SurveysCount))
            .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.User.UserName));
    }
}