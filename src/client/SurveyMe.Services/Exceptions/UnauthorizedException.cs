namespace SurveyMe.Services.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
    
    public UnauthorizedException(string message, Exception inner) : base(message, inner) { }
}