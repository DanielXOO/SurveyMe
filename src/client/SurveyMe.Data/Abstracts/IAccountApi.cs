using Refit;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Data.Abstracts;

public interface IAccountApi
{
    [Post("/account/login")]
    Task<string> LoginAsync([Body]UserLoginRequestModel user);

    [Post("/account/registration")]
    Task RegistrationAsync([Body]UserRegistrationRequestModel userModel);
}