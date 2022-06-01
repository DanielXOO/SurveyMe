using SurveyMe.DomainModels.Authentication;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Services.Abstracts;

public interface IAccountService
{
    Task<JwtToken> LoginAsync(AuthenticationRequestModel user);

    Task RegistrationAsync(UserRegistrationRequestModel userModel);
}