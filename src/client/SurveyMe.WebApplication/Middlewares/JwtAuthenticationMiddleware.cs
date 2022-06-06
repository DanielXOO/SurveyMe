using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SurveyMe.WebApplication.Middlewares;

public sealed class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;


    public JwtAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        var hasToken = context.Request.Cookies.TryGetValue("X-Access-Token", out var accessToken);
        
        if (!hasToken)
        {
            context.Response.Redirect($"/Account/Login");
            return;
        }

        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.ReadToken(accessToken) as JwtSecurityToken;

        var name = securityToken.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value; 
        var role = securityToken.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, role)
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        
        await context.SignInAsync(principal);

        await _next(context);
    }
}