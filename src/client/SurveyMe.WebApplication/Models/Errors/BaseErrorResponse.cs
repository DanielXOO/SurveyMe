namespace SurveyMe.WebApplication.Models.Errors;

public class BaseErrorResponse
{
    public int StatusCode { get; set; }
    
    public string Message { get; set; }

    public List<string> Details { get; set; }
}