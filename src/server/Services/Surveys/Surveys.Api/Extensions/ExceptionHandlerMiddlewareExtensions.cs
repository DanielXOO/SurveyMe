using Surveys.Api.Middleware;

namespace Surveys.Api.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ErrorsHandleMiddleware>();  
    }  
}