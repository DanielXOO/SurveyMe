using Refit;
using SurveyMe.DomainModels.Authentication;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Data.Abstracts;

public interface IAccountApi
{
    [Post("/account/login")]
    Task<JwtToken> LoginAsync([Body]UserLoginRequestModel user);

    [Post("/account/registration")]
    Task RegistrationAsync([Body]UserRegistrationRequestModel userModel);
}