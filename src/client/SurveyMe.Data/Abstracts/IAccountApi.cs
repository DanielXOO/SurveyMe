using Refit;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Data.Abstracts;

public interface IAccountApi
{
    [Post("/account/login")]
    Task<string> Login([Body]UserLoginRequestModel user);

    [Post("/account/registration")]
    Task Registration([Body]UserRegistrationRequestModel userModel);
}