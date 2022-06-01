using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Authentication.Api.Models.Errors;
using Authentication.Api.Models.Exceptions;
using Microsoft.AspNetCore.WebUtilities;

namespace Authentication.Api.Middleware;

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

            var error = HandleErrorAsync(ex, StatusCodes.Status400BadRequest);
            await SendErrorResponse(context, error);
        }
        catch (NotFoundException ex)
        {
            _logger.LogCritical(ex, "Not found error");

            var error = HandleErrorAsync(ex, StatusCodes.Status404NotFound);
            await SendErrorResponse(context, error);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogCritical(ex, "Bad request error");

            var error = HandleErrorAsync(ex, StatusCodes.Status400BadRequest);
            await SendErrorResponse(context, error);
        }
        catch (ForbidException ex)
        {
            _logger.LogCritical(ex, "User has not access");

            var error = HandleErrorAsync(ex, StatusCodes.Status403Forbidden);
            await SendErrorResponse(context, error);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Server error");

            var error = HandleErrorAsync(ex, StatusCodes.Status500InternalServerError);
            await SendErrorResponse(context, error);
        }
    }


    private static BaseErrorResponse HandleErrorAsync(Exception ex, int statusCode)
    {
        var errorResponse = new BaseErrorResponse
        {
            StatusCode = statusCode,
            Message =  ReasonPhrases.GetReasonPhrase(statusCode),
            Details = ex.Message
        };
        
        if (ex.InnerException != null)
        {
            errorResponse.InnerError = HandleErrorAsync(ex.InnerException, statusCode);
        }
        
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
}