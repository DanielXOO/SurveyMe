using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.AspNetCore.WebUtilities;
using Refit;
using SurveyMe.Services.Exceptions;
using SurveyMe.WebApplication.Models.Errors;

namespace SurveyMe.WebApplication.Middlewares;

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
            context.Response.Redirect($"/Errors?code={error.StatusCode}");
        }
        catch (NotFoundException ex)
        {
            _logger.LogCritical(ex, "Not found error");
            
            var error = HandleErrorAsync(ex, StatusCodes.Status404NotFound);
            context.Response.Redirect($"/Errors?code={error.StatusCode}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogCritical(ex, "Bad request error");
            
            var error = HandleErrorAsync(ex, StatusCodes.Status400BadRequest);
            context.Response.Redirect($"/Errors?code={error.StatusCode}");
        }
        catch (ApiException ex)
        {
            _logger.LogCritical(ex, "Api error");

            var error = HandleErrorAsync(ex, (int)ex.StatusCode);
            error.Message = ex.Content;

            switch (ex.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    context.Response.Redirect("/Account/Login");
                    break;
                default:
                    context.Response.Redirect($"/Errors?code={(int)ex.StatusCode}");
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Server error");
            var error = HandleErrorAsync(ex, StatusCodes.Status500InternalServerError);
            
            context.Response.Redirect($"/Errors?code={error.StatusCode}");
        }
    }


    /// <summary>
    /// Map exception into BaseError model
    /// </summary>
    /// <param name="ex">exception</param>
    /// <param name="statusCode">http error status code</param>
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
}