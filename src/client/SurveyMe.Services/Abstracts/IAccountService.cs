using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.Services.Abstracts;

public interface IAccountService
{
    Task<string> LoginAsync(UserLoginRequestModel user);

    Task RegistrationAsync(UserRegistrationRequestModel userModel);
}