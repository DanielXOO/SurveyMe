namespace Surveys.Api.Models.Response.Errors;

public class BadRequestErrorResponse 
{
    public int StatusCode { get; set; }

    public string Message { get; set; }
    
    public Dictionary<string, string[]> Details { get; set; }
}