using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Authentication.Api.Configurations;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new("SurveyMeApi",  new[] {
            JwtClaimTypes.Name, 
            JwtClaimTypes.Id,
            JwtClaimTypes.Role
        })
    };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new()
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes =
                {
                    "SurveyMeApi"
                },
                AccessTokenType = AccessTokenType.Jwt,
                RequireClientSecret = false
            }
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new("SurveyMeApi", new[]
            {
                JwtClaimTypes.Name, 
                JwtClaimTypes.Id,
                JwtClaimTypes.Role
            })
        };
}