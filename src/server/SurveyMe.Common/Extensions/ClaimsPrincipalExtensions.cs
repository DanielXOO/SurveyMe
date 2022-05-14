using System;
using System.Security.Claims;
using SurveyMe.Common.Exceptions;

namespace SurveyMe.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claims)
    {
        var id = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id == null)
        {
            throw new ForbidException();
        }
        
        return Guid.Parse(id);
    }
}