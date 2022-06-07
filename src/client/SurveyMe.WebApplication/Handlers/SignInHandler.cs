using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SurveyMe.DomainModels.Authentication;

namespace SurveyMe.WebApplication.Handlers;

public class SignInHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;
    
    
    public SignInHandler(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return response;
        }

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrEmpty(responseBody))
        {
            return response;
        }
        
        var tokens = JsonSerializer.Deserialize<JwtToken>(responseBody);
        
        SetAuthenticationCookies(tokens);
        
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.ReadToken(tokens.AccessToken) as JwtSecurityToken;

        var name = securityToken.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value; 
        var role = securityToken.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, role)
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        
        await _accessor.HttpContext.SignInAsync(principal);
        
        return response;
    }
    
    
    private void SetAuthenticationCookies(JwtToken token)
    {
        _accessor.HttpContext.Response.Cookies.Append("X-Access-Token", token.AccessToken,
            new CookieOptions 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict,
            });
            
        _accessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", token.RefreshToken,
            new CookieOptions 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict
            });
    }
}