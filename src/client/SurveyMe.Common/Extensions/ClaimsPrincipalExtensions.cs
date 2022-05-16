using System.Security.Claims;

namespace SurveyMe.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claims)
    {
        var id = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Guid.Parse(id);
    }
}