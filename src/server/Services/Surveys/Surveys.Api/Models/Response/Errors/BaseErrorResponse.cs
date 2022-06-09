namespace Surveys.Api.Models.Response.Errors;

public class BaseErrorResponse
{
    public int StatusCode { get; set; }
    
    public string Message { get; set; }

    public string Details { get; set; }

    public BaseErrorResponse InnerError { get; set; }
}