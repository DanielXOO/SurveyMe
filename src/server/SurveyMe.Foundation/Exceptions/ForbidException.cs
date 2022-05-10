using System;

namespace SurveyMe.Foundation.Exceptions;

public class ForbidException : Exception
{
    public ForbidException(string message) : base(message) { }
    
    public ForbidException(string message, Exception inner) : base(message, inner) { }
}