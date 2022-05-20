using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyMe.DomainModels.Users;
using SurveyMe.Foundation.Models.Configurations;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public sealed class TokenGenerator : ITokenGenerator
{
    private readonly TokenGeneratorConfiguration _configuration;


    public TokenGenerator(IOptions<TokenGeneratorConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }
    
    
    public string GenerateToken(User user)
    {
        var symmetricSecurityKey = _configuration.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        
        foreach (var userRole in user.Roles)
        {
            var claim = new Claim(ClaimTypes.Role, userRole.Name);
            claims.Add(claim);
        }
        
        //TODO: Add jwt configuration
        var token = new JwtSecurityToken(_configuration.Issuer, _configuration.Issuer, 
            claims, DateTime.Now, DateTime.Now.AddDays(1), credentials);
        
        var encodedJwt =  new JwtSecurityTokenHandler().WriteToken(token);

        return encodedJwt;
    }
}