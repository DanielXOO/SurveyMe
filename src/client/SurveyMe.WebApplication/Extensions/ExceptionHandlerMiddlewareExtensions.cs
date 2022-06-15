using SurveyMe.WebApplication.Middlewares;

namespace SurveyMe.WebApplication.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ErrorsHandleMiddleware>();  
    }  
}