using Authentication.Api.Middleware;

namespace Authentication.Api.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ErrorsHandleMiddleware>();  
    }  
}