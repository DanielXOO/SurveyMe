using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using SurveyMe.Surveys.Foundation.Exceptions;
using SurveyMe.WebApplication.Models.Errors;

namespace SurveyMe.WebApplication.Middleware;

public sealed class ErrorsHandleMiddleware
{
    private readonly RequestDelegate _next;
    
    private readonly ILogger<ErrorsHandleMiddleware> _logger;

    
    public ErrorsHandleMiddleware(RequestDelegate next, ILogger<ErrorsHandleMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Something gone wrong");
            await HandleErrorAsync(context, ex);
        }
    }


    
    private static async Task HandleErrorAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = ex switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new BaseErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = ReasonPhrases.GetReasonPhrase(context.Response.StatusCode),
            Details = new []{ ex.Message }
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        
        await context.Response.WriteAsync(jsonResponse);
    }
}