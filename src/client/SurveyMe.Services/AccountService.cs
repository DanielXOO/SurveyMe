using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public class AccountService : IAccountService
{
    private readonly IAccountApi _accountApi;


    public AccountService(IAccountApi accountApi)
    {
        _accountApi = accountApi;
    }

    
    public async Task<string> LoginAsync(UserLoginRequestModel user)
    {
        var token = await _accountApi.LoginAsync(user);

        return token;
    }

    public async Task RegistrationAsync(UserRegistrationRequestModel userModel)
    {
        await _accountApi.RegistrationAsync(userModel);
    }
}