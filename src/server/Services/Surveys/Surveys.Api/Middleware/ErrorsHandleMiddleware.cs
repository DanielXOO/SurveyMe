using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.AspNetCore.WebUtilities;
using SurveyMe.Common.Exceptions;
using Surveys.Api.Models.Response.Errors;

namespace Surveys.Api.Middleware;

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

            var error = HandleError(ex);
            await SendErrorResponse(context, error);
        }
        catch (NotFoundException ex)
        {
            _logger.LogCritical(ex, "Not found error");

            var error = HandleError(ex, StatusCodes.Status404NotFound);
            await SendErrorResponse(context, error);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogCritical(ex, "Bad request error");

            var error = HandleError(ex, StatusCodes.Status400BadRequest);
            await SendErrorResponse(context, error);
        }
        catch (ForbidException ex)
        {
            _logger.LogCritical(ex, "User has not access");

            var error = HandleError(ex, StatusCodes.Status403Forbidden);
            await SendErrorResponse(context, error);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Server error");

            var error = HandleError(ex, StatusCodes.Status500InternalServerError);
            await SendErrorResponse(context, error);
        }
    }


    private static BaseErrorResponse HandleError(Exception ex, int statusCode)
    {
        var errorResponse = new BaseErrorResponse
        {
            StatusCode = statusCode,
            Message =  ReasonPhrases.GetReasonPhrase(statusCode),
            Details = ex.Message
        };
        
        if (ex.InnerException != null)
        {
            errorResponse.InnerError = HandleError(ex.InnerException, statusCode);
        }
        
        return errorResponse;
    }
    
    private static BadRequestErrorResponse HandleError(BadRequestException ex)
    {
        var errorResponse = new BadRequestErrorResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Details = ex.Error,
            Message = ex.Message
        };
        
        return errorResponse;
    }

    /// <summary>
    /// Send server response with info about exception
    /// </summary>
    /// <param name="context"></param>
    /// <param name="errorResponse"></param>
    private static async Task SendErrorResponse(HttpContext context, BaseErrorResponse errorResponse)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;
        var jsonResponse = JsonSerializer.Serialize(errorResponse, options);
        
        await context.Response.WriteAsync(jsonResponse);
    }
    
    /// <summary>
    /// Overload for bad request error
    /// </summary>
    /// <param name="context"></param>
    /// <param name="errorResponse"></param>
    private static async Task SendErrorResponse(HttpContext context, BadRequestErrorResponse errorResponse)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;
        var jsonResponse = JsonSerializer.Serialize(errorResponse, options);
        
        await context.Response.WriteAsync(jsonResponse);
    }
}