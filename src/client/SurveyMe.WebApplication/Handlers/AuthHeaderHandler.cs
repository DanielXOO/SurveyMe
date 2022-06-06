using System.Net;
using System.Net.Http.Headers;
using SurveyMe.DomainModels.Authentication;
using SurveyMe.Services.Abstracts;
using SurveyMe.Services.Exceptions;

namespace SurveyMe.WebApplication.Handlers;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IAccountService _accountService;
    
    
    public AuthHeaderHandler(IHttpContextAccessor accessor, IAccountService accountService)
    {
        _accessor = accessor;
        _accountService = accountService;
    }
    
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var hasAccessToken = _accessor.HttpContext.Request.Cookies
            .TryGetValue("X-Access-Token", out var accessToken);

        if (!hasAccessToken)
        {
            throw new UnauthorizedException("No access");
        }
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            _accessor.HttpContext.Request.Cookies
                .TryGetValue("X-Refresh-Token", out var refreshToken);

            var newToken = await _accountService.RefreshTokenAsync(refreshToken);
            
            RefreshCookies(newToken);
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken.AccessToken);
            
            response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        
        return response;
    }

    private void RefreshCookies(JwtToken token)
    {
        _accessor.HttpContext.Response.Cookies.Append("X-Access-Token", token.AccessToken,
            new CookieOptions 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddSeconds(5)
            });
            
        _accessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", token.RefreshToken,
            new CookieOptions 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict
            });
    }
}