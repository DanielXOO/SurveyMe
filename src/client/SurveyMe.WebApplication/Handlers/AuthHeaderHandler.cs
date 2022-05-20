using System.Net.Http.Headers;

namespace SurveyMe.WebApplication.Handlers;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;
    
    
    public AuthHeaderHandler(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _accessor.HttpContext.Request.Cookies.TryGetValue("X-Access-Token", out var token);
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}