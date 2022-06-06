using Refit;
using SurveyMe.DomainModels.Authentication;
using SurveyMe.DomainModels.Request.Authentication;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Data.Abstracts;

public interface IAccountApi
{
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/connect/token")]
    Task<JwtToken> LoginAsync([Body(BodySerializationMethod.UrlEncoded)]AuthenticationRequestModel user);
    
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/connect/token")]
    Task<JwtToken> RefreshTokenAsync([Body(BodySerializationMethod.UrlEncoded)]RefreshTokenRequestModel accessToken);

    [Post("/authentication/registration")]
    Task RegistrationAsync([Body]UserRegistrationRequestModel userModel);
}