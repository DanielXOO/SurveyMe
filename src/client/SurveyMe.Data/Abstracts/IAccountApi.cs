using Refit;
using SurveyMe.DomainModels.Authentication;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Data.Abstracts;

public interface IAccountApi
{
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/connect/token")]
    Task<JwtToken> LoginAsync([Body(BodySerializationMethod.UrlEncoded)]AuthenticationRequestModel user);

    [Post("/account/registration")]
    Task RegistrationAsync([Body]UserRegistrationRequestModel userModel);
}