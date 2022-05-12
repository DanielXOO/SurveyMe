using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public class AccountApi : IAccountService
{
    private readonly IClient _client;
    private const string ServiceBasePath = "/account";
    
    public AccountApi(IClient client)
    {
        _client = client;
    }
    
    
    public async Task LoginAsync(UserLoginRequestModel user)
    {
        var url = "account/login";

        await _client.SendPostRequestAsync(url, user);
    }

    public async Task RegistrationAsync(UserRegistrationRequestModel user)
    {
        throw new NotImplementedException();
    }
}