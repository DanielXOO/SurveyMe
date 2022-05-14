using SurveyMe.DomainModels.Request;

namespace SurveyMe.Services.Abstracts;

public interface IAccountService
{
    Task LoginAsync(UserLoginRequestModel user);

    Task RegistrationAsync(UserRegistrationRequestModel user);
}