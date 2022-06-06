using SurveyMe.DomainModels.Authentication;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Services.Abstracts;

public interface IAccountService
{
    Task<JwtToken> LoginAsync(UserLoginRequestModel user);

    Task<JwtToken> RefreshTokenAsync(string refreshToken);

    Task RegistrationAsync(UserRegistrationRequestModel userModel);
}