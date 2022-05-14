using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public class AccountService : IAccountService
{
    private readonly IUserApi _userApi;
    private const string ServiceBasePath = "/account";
    
    public AccountService(IUserApi userApi)
    {
        _userApi = userApi;
    }
    
    
    public async Task LoginAsync(UserLoginRequestModel user)
    {
        var url = "account/login";
        throw new NotImplementedException();
    }

    public async Task RegistrationAsync(UserRegistrationRequestModel user)
    {
        throw new NotImplementedException();
    }
}