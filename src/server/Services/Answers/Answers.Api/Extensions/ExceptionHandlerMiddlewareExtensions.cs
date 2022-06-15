using Answers.Api.Middleware;

namespace Answers.Api.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ErrorsHandleMiddleware>();  
    }  
}