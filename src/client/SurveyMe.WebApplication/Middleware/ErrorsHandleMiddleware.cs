using System.Net.Sockets;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using SurveyMe.Common.Exceptions;
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
        catch (BadRequestException ex)
        {
            _logger.LogCritical(ex, "Bad request error");
            await HandleErrorAsync(context, ex, StatusCodes.Status400BadRequest);
        }
        catch (NotFoundException ex)
        {
            _logger.LogCritical(ex, "Not found error");
            await HandleErrorAsync(context, ex, StatusCodes.Status404NotFound);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogCritical(ex, "Bad request error");
            await HandleErrorAsync(context, ex, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Server error");
            await HandleErrorAsync(context, ex, StatusCodes.Status500InternalServerError);
        }
    }


    /// <summary>
    /// Handle exceptions and send response with error code
    /// </summary>
    /// <param name="context">http context</param>
    /// <param name="ex">exception</param>
    /// <param name="statusCode">http error status code</param>
    private static async Task HandleErrorAsync(HttpContext context, Exception ex, int statusCode)
    {
        context.Response.ContentType = "application/json;charset=utf-8";

        context.Response.StatusCode = statusCode;

        var response = new BaseErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = ReasonPhrases.GetReasonPhrase(context.Response.StatusCode),
            Details = new List<string>()
        };
        
        while (ex != null)
        {
            response.Details.Add(ex.Message);
            ex = ex.InnerException;
        }

        var jsonResponse = JsonSerializer.Serialize(response);
        
        await context.Response.WriteAsync(jsonResponse);
    }
}