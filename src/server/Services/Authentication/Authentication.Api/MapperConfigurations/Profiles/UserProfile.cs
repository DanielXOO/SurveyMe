using Authentication.Api.Models.Request.Users;
using Authentication.Users;
using AutoMapper;

namespace Authentication.Api.MapperConfigurations.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationRequestModel, User>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.DisplayName,
                opt => opt.MapFrom(src => src.Name));
    }
}