namespace Authentication.Api.Models.Exceptions;

public class ForbidException : Exception
{
    public ForbidException(string message) : base(message) { }
    
    public ForbidException() { }
    
    public ForbidException(string message, Exception inner) : base(message, inner) { }
}