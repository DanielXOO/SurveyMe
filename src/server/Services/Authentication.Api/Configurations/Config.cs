﻿using IdentityModel;
using IdentityServer4.Models;

namespace Authentication.Api.Configurations;

public static class Config
{
    public static IEnumerable<IdentityResource> Resources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId()
        };

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
                ClientSecrets =
                {
                    new Secret("client_secret".Sha256())
                }
            }
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new("SurveyMeApi")
            {
                ApiSecrets =
                {
                    new Secret("api_secret".Sha256())
                },
                Scopes = { "SurveyMeApi" }
            }
        };
}