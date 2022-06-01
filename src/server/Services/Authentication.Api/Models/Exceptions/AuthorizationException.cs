namespace Authentication.Api.Models.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException() { }

    public AuthorizationException(string message) : base(message) { }
}