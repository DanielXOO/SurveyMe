using System;

namespace SurveyMe.Foundation.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException() { }

    public AuthorizationException(string message) : base(message) { }
}