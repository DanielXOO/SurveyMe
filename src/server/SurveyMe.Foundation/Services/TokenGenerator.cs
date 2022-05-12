using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public sealed class TokenGenerator : ITokenGenerator
{
    public string GenerateToken(User user)
    {
        //TODO: Add secret key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3PQgjM3K3d4NG6TaT5xy5lq2zmE6gX8CPP6xrlgAKUDDjnpQPX6ThGIqikETvq3ncl5TNQKEfRBhubqVhNC3lQWzvc8CODtde8Xeohm1sCgyW5EX-ncp7pUdrw268d-lQqrEf-G8rReKM6sDfWoqIY2JCvunDfJxv8Sw5ah6LT0"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

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
        var token = new JwtSecurityToken("issuer", "audience", claims, DateTime.Now, DateTime.Now.AddDays(1), credentials);
        var encodedJwt =  new JwtSecurityTokenHandler().WriteToken(token);

        return encodedJwt;
    }
}