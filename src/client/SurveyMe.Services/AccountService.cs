using Microsoft.Extensions.Options;
using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Authentication;
using SurveyMe.DomainModels.Request.Authentication;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public class AccountService : IAccountService
{
    private readonly IAccountApi _accountApi;
    
    private readonly IdentityServerConfiguration _configuration;

    
    public AccountService(IAccountApi accountApi, IOptions<IdentityServerConfiguration> configuration)
    {
        _accountApi = accountApi;
        _configuration = configuration.Value;
    }

    
    public async Task<JwtToken> LoginAsync(UserLoginRequestModel user)
    {
        var request = new AuthenticationRequestModel
        {
            UserName = user.Login,
            Password = user.Password,
            ClientId = _configuration.ClientId,
            Scope = _configuration.Scope,
            ClientSecret = _configuration.ClientSecret,
            GrantType = GrantTypes.Password
        };
        
        var token = await _accountApi.LoginAsync(request);

        return token;
    }

    public async Task<JwtToken> RefreshTokenAsync(string refreshToken)
    {
        var request = new RefreshTokenRequestModel
        {
            RefreshToken = refreshToken,
            ClientId = _configuration.ClientId,
            Scope = _configuration.Scope,
            ClientSecret = _configuration.ClientSecret,
            GrantType = GrantTypes.RefreshToken
        };

        var newToken = await _accountApi.RefreshTokenAsync(request);

        return newToken;
    }

    public async Task RegistrationAsync(UserRegistrationRequestModel userModel)
    {
        await _accountApi.RegistrationAsync(userModel);
    }
}